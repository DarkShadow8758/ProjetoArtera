using UnityEngine;

public class NPCLookAtPlayer : MonoBehaviour
{
    [Tooltip("Velocidade de rotação do NPC")]
    public float rotationSpeed = 5f;

    [Tooltip("Se verdadeiro, o NPC vai rotacionar suavemente")]
    public bool smoothRotation = true;

    [Tooltip("Offset de rotação em graus no eixo Y (ajuste para controlar para onde o NPC olha)")]
    public float yRotationOffset = 0f;

    private Transform player;

    [HideInInspector] public bool isDetected = false;

    void Update()
    {
        if (!isDetected || player == null)
            return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion baseRotation = Quaternion.LookRotation(direction);
            Vector3 eulerAngles = baseRotation.eulerAngles;
            eulerAngles.y += yRotationOffset;
            Quaternion targetRotation = Quaternion.Euler(eulerAngles);

            if (smoothRotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = targetRotation;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            isDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDetected = false;
            player = null;
        }
    }
}
