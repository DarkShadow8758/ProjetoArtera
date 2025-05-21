using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class VisitPillarsQuestSep : QuestStep
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }
    protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
