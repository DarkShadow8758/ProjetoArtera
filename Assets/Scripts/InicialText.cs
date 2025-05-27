using System.Collections;
using UnityEngine;
using TMPro;

public class TextFadeAnimation : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private TextMeshProUGUI textMeshPro;

    [Header("Configurações de Tempo")]
    [SerializeField] private float delayAntesDeFadeIn = 0.5f;
    [SerializeField] private float duracaoFadeIn = 1.5f;
    [SerializeField] private float duracaoExibicao = 3.0f;
    [SerializeField] private float duracaoFadeOut = 1.5f;

    [Header("Configurações de Animação")]
    [SerializeField] private AnimationCurve curvaFadeIn = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private AnimationCurve curvaFadeOut = AnimationCurve.EaseInOut(0, 1, 1, 0);
    [SerializeField] private Color corInicial = Color.white;
    [SerializeField] private bool desativarAposFade = true;
    [SerializeField] private bool escalaTextoComFade = false;
    [SerializeField] private Vector3 escalaFinal = Vector3.one;
    [SerializeField] private Vector3 escalaInicial = new Vector3(0.8f, 0.8f, 0.8f);

    private void Awake()
    {
        // Se a referência não for definida no Inspector, tenta pegar do próprio objeto
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }
        
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro não encontrado! Adicione um componente TextMeshPro ou defina uma referência.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        // Inicia a animação uma vez quando o jogo começar
        StartCoroutine(AnimacaoFadeCompleta());
    }

    private IEnumerator AnimacaoFadeCompleta()
    {
        // Configuração inicial
        Color corAtual = corInicial;
        corAtual.a = 0f;
        textMeshPro.color = corAtual;
        
        if (escalaTextoComFade)
        {
            textMeshPro.transform.localScale = escalaInicial;
        }

        // Espera pelo delay inicial
        yield return new WaitForSeconds(delayAntesDeFadeIn);

        // Fade In
        float tempo = 0f;
        while (tempo < duracaoFadeIn)
        {
            float valorNormalizado = tempo / duracaoFadeIn;
            float alpha = curvaFadeIn.Evaluate(valorNormalizado);
            
            corAtual.a = alpha;
            textMeshPro.color = corAtual;
            
            if (escalaTextoComFade)
            {
                textMeshPro.transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, alpha);
            }
            
            tempo += Time.deltaTime;
            yield return null;
        }
        
        // Garante que o fade in chegue a 100%
        corAtual.a = 1f;
        textMeshPro.color = corAtual;
        if (escalaTextoComFade)
        {
            textMeshPro.transform.localScale = escalaFinal;
        }

        // Mantém o texto visível pelo tempo de exibição
        yield return new WaitForSeconds(duracaoExibicao);

        // Fade Out
        tempo = 0f;
        while (tempo < duracaoFadeOut)
        {
            float valorNormalizado = tempo / duracaoFadeOut;
            float alpha = curvaFadeOut.Evaluate(valorNormalizado);
            
            corAtual.a = alpha;
            textMeshPro.color = corAtual;
            
            if (escalaTextoComFade)
            {
                textMeshPro.transform.localScale = Vector3.Lerp(escalaFinal, escalaInicial, valorNormalizado);
            }
            
            tempo += Time.deltaTime;
            yield return null;
        }
        
        // Garante que o fade out chegue a 0%
        corAtual.a = 0f;
        textMeshPro.color = corAtual;
        
        // Desativa o objeto se configurado
        if (desativarAposFade)
        {
            gameObject.SetActive(false);
        }
    }

    // Método público para iniciar a animação de fora da classe
    public void IniciarAnimacao()
    {
        gameObject.SetActive(true);
        StartCoroutine(AnimacaoFadeCompleta());
    }
}