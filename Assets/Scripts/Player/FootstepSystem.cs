using UnityEngine;

public class FootstepSystem : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] footstepSounds;
    public AudioClip footstepGround;
    public AudioClip[] jumpSounds;

    RaycastHit hit;
    public Transform RayStart;
    public float range;
    public LayerMask groundLayer;

    public void Footstep()
    {
        if (Physics.Raycast(RayStart.position, Vector3.down, out hit, range, groundLayer))
        {
            switch (hit.collider.tag)
            {
                case "ground":
                    PlayFootstepSoundL(footstepGround);
                    break;
                case "water":
                    // Play water sound
                    break;
                case "mud":
                    // Play mud sound
                    break;
                default:
                    break;
            }
            if (hit.collider.CompareTag("ground"))
            {
                int randomIndex = Random.Range(0, footstepSounds.Length);
                audioSource.PlayOneShot(footstepSounds[randomIndex]);
            }
        }
    }
    void PlayFootstepSoundL(AudioClip audio)
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(audio);
    }
}
