using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField] bool cutsceneDialogue = false;
    private bool activated = false;

    private GameObject player=null;

    void Start() {
        if(cutsceneDialogue) { TriggerDialogue(); }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !activated)
        {
            player=other.gameObject;
            activated = true;
            TriggerDialogue();
        }
    }

    void FixedUpdate()
    {
        if(player!=null && activated)
        {
            if(!player.GetComponent<Controller>().isActive)
            {
                activated=false;
                FindObjectOfType<DialogueManager>().EndDialogue();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && activated)
        {
            player=null;
            activated = false;
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }
}
