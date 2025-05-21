using UnityEngine;

public class DistanceLightController : MonoBehaviour
{
    [Header("Referências")]
    [Tooltip("A luz que será controlada")]
    public Light targetLight;

    [Tooltip("O transform do jogador")]
    public Transform player;

    [Header("Configurações da Intensidade")]
    [Tooltip("Intensidade máxima da luz quando o jogador está longe")]
    public float maxIntensity = 100f;

    [Tooltip("Intensidade mínima da luz (quando jogador está próximo)")]
    public float minIntensity = 30f;

    [Header("Configurações do Range/Alcance")]
    [Tooltip("Range máximo da luz quando o jogador está longe")]
    public float maxRange = 100f;

    [Tooltip("Range mínimo da luz (quando jogador está próximo)")]
    public float minRange = 1.5f;

    [Header("Configurações Gerais")]
    [Tooltip("Distância máxima para cálculo dos efeitos")]
    public float maxDistance = 10f;

    [Tooltip("Fator para curva exponencial (valores maiores = queda mais rápida)")]
    [Range(1.0f, 5.0f)]
    public float exponentialFactor = 2.0f;

    [Tooltip("Visualizar curva exponencial no Debug")]
    public bool showDebugInfo = false;

    private void Start()
    {
        // Verificações para evitar erros
        if (targetLight == null)
        {
            Debug.LogError("Luz não atribuída no componente DistanceLightController!");
            enabled = false;
            return;
        }

        if (player == null)
        {
            // Tenta encontrar o jogador automaticamente
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            if (player == null)
            {
                Debug.LogError("Transform do jogador não atribuído e não foi possível encontrá-lo automaticamente!");
                enabled = false;
                return;
            }
        }

        // Inicializa a luz com a intensidade e range máximos
        targetLight.intensity = maxIntensity;
        targetLight.range = maxRange;
    }

    private void Update()
    {
        // Calcula a distância entre o jogador e a luz
        float distance = Vector3.Distance(player.position, transform.position);

        // Limita a distância ao valor máximo definido
        distance = Mathf.Min(distance, maxDistance);

        // Calcula o fator de distância normalizado (0 a 1)
        float normalizedDistance = distance / maxDistance;

        // Aplica função exponencial para que a queda seja mais rápida próximo da luz
        // Pow(x, n) onde n > 1 faz a curva cair mais rapidamente quando x se aproxima de 0
        float exponentialCurve = Mathf.Pow(normalizedDistance, exponentialFactor);

        // Calcula os novos valores baseados na curva exponencial
        float newIntensity = Mathf.Lerp(minIntensity, maxIntensity, exponentialCurve);
        float newRange = Mathf.Lerp(minRange, maxRange, exponentialCurve);

        // Aplica os novos valores à luz
        targetLight.intensity = newIntensity;
        targetLight.range = newRange;

        // Debug info
        if (showDebugInfo)
        {
            Debug.DrawLine(transform.position, player.position, Color.red);

            if (Input.GetKeyDown(KeyCode.L) || Time.frameCount % 60 == 0) // A cada segundo ou ao pressionar L
            {
                Debug.Log($"Distância: {distance:F2} / {maxDistance:F2} ({normalizedDistance:F2})" +
                          $"\nCurva Exponencial: {exponentialCurve:F3}" +
                          $"\nIntensidade: {newIntensity:F2} / {maxIntensity:F2}" +
                          $"\nRange: {newRange:F2} / {maxRange:F2}");
            }
        }
    }

    // Mostra graficamente como a função exponencial afeta os valores
    private void OnGUI()
    {
        if (!showDebugInfo) return;

        // Desenha uma pequena janela com a representação da curva exponencial
        int width = 200;
        int height = 100;
        int margin = 10;

        GUI.Box(new Rect(margin, margin, width + 10, height + 30), "Curva Exponencial");

        // Desenha a curva
        for (int x = 0; x < width; x++)
        {
            float normalizedX = (float)x / width;
            float exponentialY = Mathf.Pow(normalizedX, exponentialFactor);
            float y1 = height - exponentialY * height;

            // Linha mais grossa para melhor visualização
            for (int thickness = 0; thickness < 3; thickness++)
            {
                GUI.Box(new Rect(margin + 5 + x, margin + 20 + y1 - thickness, 1, 1), "");
            }
        }

        // Desenha linha de referência (linear)
        for (int x = 0; x < width; x++)
        {
            float normalizedX = (float)x / width;
            float y1 = height - normalizedX * height;

            GUI.contentColor = Color.grey;
            GUI.Box(new Rect(margin + 5 + x, margin + 20 + y1, 1, 1), "");
        }

        // Restaura a cor
        GUI.contentColor = Color.white;
    }

    // Método para visualizar o raio de detecção no editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}