using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range;
    public Transform centrePoint;

    [Header("Controle de Movimento")]
    public bool enableRandomMovement = true;

    [Header("Rotação")]
    public float yRotationOffset = 0f;

    // Referência para o script NPCLookAtPlayer
    private NPCLookAtPlayer npcLookAtPlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        // Tenta obter automaticamente o componente NPCLookAtPlayer no mesmo GameObject
        npcLookAtPlayer = GetComponent<NPCLookAtPlayer>();
        if (npcLookAtPlayer == null)
        {
            Debug.LogWarning("NPCLookAtPlayer não encontrado no GameObject! Certifique-se de que está no mesmo objeto.");
        }
    }

    void Update()
    {
        // Se o script de look estiver presente e detectar o player, interrompe o movimento aleatório
        if (npcLookAtPlayer != null && npcLookAtPlayer.isDetected)
        {
            return;
        }

        if (enableRandomMovement && agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
            }
        }

        RotateTowardsMovementDirection();
    }

    void RotateTowardsMovementDirection()
    {
        Vector3 velocity = agent.desiredVelocity;

        if (velocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            Vector3 euler = targetRotation.eulerAngles;
            euler.x = 0f;
            euler.z = 0f;
            euler.y += yRotationOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), Time.deltaTime * 5f);
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
