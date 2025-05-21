using UnityEngine;

[System.Serializable]
public class QuestData
{
    public QuestState state;
    public int quesStepIndex;
    public QuestStepState[] questStepStates;

    public QuestData(QuestState state, int questStepIndex, QuestStepState[] questStepStates)
    {
        this.state = state;
        this.quesStepIndex = questStepIndex;
        this.questStepStates = questStepStates;
    }
}
