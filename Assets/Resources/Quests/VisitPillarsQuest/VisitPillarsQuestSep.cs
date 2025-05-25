using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class VisitPillarsQuestSep : QuestStep
{
    [Header("Config")]
    [SerializeField] private string pillarNumbersString = "primeiro";

    private void Start()
    {
        string status = "Visite o " + pillarNumbersString + " pilar.";
        ChangeState("", status);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string status = "O " + pillarNumbersString + " pilar foi visitado";
            ChangeState("", status);
            FinishQuestStep();
        }
    }
    protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
