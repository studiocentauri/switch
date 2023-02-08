using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField] bool cutsceneDialogue = false;
    private bool activated = false;

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
            activated = true;
            TriggerDialogue();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && activated)
        {
            activated = false;
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }
}
