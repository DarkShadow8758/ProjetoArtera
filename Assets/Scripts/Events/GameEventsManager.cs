using Unity.VisualScripting;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }
    public DialogueEvents dialogueEvents;

    public void Awake()
    {
         if (instance != null)
        {
            Debug.Log("InstanceActionInvoker is not null, invoking action.");
        }
        instance = this;

        //initiate all events
        dialogueEvents = new DialogueEvents();

    } 
   
}
