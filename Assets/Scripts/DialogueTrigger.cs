using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField] bool cutsceneDialogue = false;
    private bool activated = false;

    private GameObject player = null;

    public bool isOneTimeOnly = true;

    private float deleteTime = 0.2f;

    void Start()
    {
        if (cutsceneDialogue) { TriggerDialogue(); }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !activated)
        {
            player = other.gameObject;
            activated = true;
            Debug.Log(gameObject.name + dialogue.sentences[0]);
            TriggerDialogue();
        }
    }

    void FixedUpdate()
    {
        if (player != null && activated)
        {
            if (!player.GetComponent<Controller>().isActive)
            {
                activated = false;
                FindObjectOfType<DialogueManager>().EndDialogue();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && activated)
        {
            player = null;
            activated = false;
            if (isOneTimeOnly)
            {
                isOneTimeOnly = false;
                Invoke("DeleteDialogue", deleteTime);
            }
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }

    void DeleteDialogue()
    {
        Destroy(this.gameObject);
    }
}
