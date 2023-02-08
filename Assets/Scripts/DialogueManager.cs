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
    private string voiceName;
    [SerializeField] bool isCutscene = false;
    [SerializeField] int next_scene_id = 0;
    [SerializeField] float TEXT_DISPLAY_SPEED = 0.05f;
    
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(!isCutscene) dialogueCanvas.SetActive(true);
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
            if(voiceName == "Narrator") {
                AudioManager.instance.PlaySound("Narrator Voice");
            }
            else if (voiceName == "Ram") {
                AudioManager.instance.PlaySound("Ram Voice");
            }
            yield return new WaitForSeconds(TEXT_DISPLAY_SPEED);
        }
        sentenceDone = true;
    }

    public void EndDialogue()
    {   
        if(!isCutscene) { // if it is not a cutscene, then it is an ingame dialogue
            dialogueCanvas.SetActive(false);
        }
        else { // else it is a cutscene dialogue
            SceneManage.instance.PlayLevel(next_scene_id);
        }
    }


    public void OnNextDialogue()
    {
        DisplayNextSentence();
    }
}
