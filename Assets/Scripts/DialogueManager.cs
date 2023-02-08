using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;
    public GameObject dialogueCanvas;
    private bool sentenceDone = true;
    private string name;
    [SerializeField] float TEXT_DISPLAY_SPEED = 0.05f;
    
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueCanvas.SetActive(true);
        sentences.Clear();
        sentenceDone = true;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        name = dialogue.name;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(!sentenceDone) {
            sentenceDone = true;
            return;
        }

        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text  = "";
        sentenceDone = false;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if(sentenceDone) {
                dialogueText.text = sentence;
                break;
            }
            if(name == "Narrator") {
                AudioManager.instance.PlaySound("Narrator Voice");
            }
            else if (name == "Ram") {
                AudioManager.instance.PlaySound("Ram Voice");
            }
            yield return new WaitForSeconds(TEXT_DISPLAY_SPEED);
        }
        sentenceDone = true;
    }

    public void EndDialogue()
    {
        dialogueCanvas.SetActive(false);
    }
}
