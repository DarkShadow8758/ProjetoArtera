using UnityEngine;

public class CubismPhotosQuestStep : QuestStep
{
    [Header("Config")]
    [SerializeField]
    private string pictureLocalName = "aa";

    private void OnEnable()
    {
        GameEventsManager.instance.photoCapture.onPhotoCapture += Capture;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.photoCapture.onPhotoCapture -= Capture;
    }

    private void Start()
    {
        string status = "Tire foto do " + pictureLocalName + ".";
        ChangeState("", status);
    }

    private void Capture(bool capture)
    {
        //FinishQuestStep();
    }
    protected override void SetQuestStepState(string state)
    {
        //throw new System.NotImplementedException();
    }
}
