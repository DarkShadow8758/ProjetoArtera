using UnityEngine;
using System.Collections;

public class TeleportSystem : MonoBehaviour
{
    [Tooltip("Ponto de destino para onde o jogador será teleportado")]
    public Transform destinationPoint;

    [Tooltip("Som que será reproduzido ao teleportar")]
    public AudioClip teleportSound;

    [Tooltip("Volume do som de teleporte")]
    [Range(0, 1)]
    public float soundVolume = 1.0f;

    [Tooltip("Partículas que serão exibidas na origem do teleporte")]
    public GameObject originEffect;

    [Tooltip("Partículas que serão exibidas no destino do teleporte")]
    public GameObject destinationEffect;

    [Tooltip("Tempo de espera antes do teleporte (em segundos)")]
    public float teleportDelay = 0.0f;

    [Tooltip("Manter a rotação original do jogador")]
    public bool keepRotation = true;

    [Tooltip("Tempo de cooldown entre teleportes (em segundos)")]
    public float cooldownTime = 1.0f;

    private bool canTeleport = true;
    private AudioSource audioSource;

    private void Start()
    {
        // Adiciona um AudioSource se o som de teleporte for definido
        if (teleportSound != null && GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = teleportSound;
            audioSource.volume = soundVolume;
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Validação do ponto de destino
        if (destinationPoint == null)
        {
            Debug.LogWarning("Teleport System: Destino não definido para " + gameObject.name + ". O teleporte não funcionará.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTeleport && destinationPoint != null)
        {
            StartCoroutine(TeleportPlayer(other.gameObject));
        }
    }

    private IEnumerator TeleportPlayer(GameObject player)
    {
        canTeleport = false;

        // Reproduz o som de teleporte
        if (audioSource != null && teleportSound != null)
        {
            audioSource.Play();
        }

        // Mostra o efeito de partículas na origem, se houver
        if (originEffect != null)
        {
            Instantiate(originEffect, player.transform.position, Quaternion.identity);
        }

        // Espera o tempo de delay configurado
        if (teleportDelay > 0)
        {
            yield return new WaitForSeconds(teleportDelay);
        }

        // Salva a rotação original, se necessário
        Quaternion originalRotation = player.transform.rotation;

        // Teleporta o jogador
        player.transform.position = destinationPoint.position;

        // Mantém ou aplica nova rotação
        if (keepRotation)
        {
            player.transform.rotation = originalRotation;
        }
        else
        {
            player.transform.rotation = destinationPoint.rotation;
        }

        // Mostra o efeito de partículas no destino, se houver
        if (destinationEffect != null)
        {
            Instantiate(destinationEffect, player.transform.position, Quaternion.identity);
        }

        // Aplica o cooldown
        yield return new WaitForSeconds(cooldownTime);
        canTeleport = true;
    }
}