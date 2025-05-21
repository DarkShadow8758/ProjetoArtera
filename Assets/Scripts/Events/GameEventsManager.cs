using Unity.VisualScripting;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }
    public InputEvents inputEvents;
    public PlayerEvents playerEvents;
    public GoldEvents goldEvents;
    public MiscEvents miscEvents;
    public DialogueEvents dialogueEvents;
    public QuestEvents questEvents;

    public void Awake()
    {
         if (instance != null)
        {
            Debug.Log("InstanceActionInvoker is not null, invoking action.");
        }
        instance = this;

        //initiate all events
        inputEvents = new InputEvents();
        playerEvents = new PlayerEvents();
        goldEvents = new GoldEvents();
        miscEvents = new MiscEvents();
        dialogueEvents = new DialogueEvents();
        questEvents = new QuestEvents();

    } 
   
}
