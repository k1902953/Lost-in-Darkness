using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dtext;
    private Queue<string> sentences;
    public Animator ani;
    //public Dialogue dialogue;
    public float responseTracker;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        //FindObjectOfType<DialogueTrigger>().TriggerDialogue(1);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Reset();
        ani.SetBool("isOpen", true);
        sentences.Clear();
        Debug.Log(dialogue.sentences[0]);

        foreach (string sentence in dialogue.sentences)
        {
            //dtext.text = dialogue.sentences[0];
            sentences.Enqueue(sentence);
        }
        /*if (responseTracker == 0 && dialogue.sentences.Length >=0)
        {
            dtext.text = dialogue.sentences[0];
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
           
        }
        else if(responseTracker == 1 && dialogue.sentences.Length >= 1)
        {
            dtext.text = dialogue.sentences[1];
            Debug.Log(dialogue.sentences[1]);
        }*/
        /*else if (responseTracker == 2 && dialogue.sentences.Length >= 2)
        {
            dtext.text = dialogue.sentences[2];

        }*/
        DisplayNextSentence();
    }

    void Reset()
    {
        ani.SetBool("isOpen", true);
        sentences.Clear();
        responseTracker = 0;
        //dtext.text = dialogue.sentences[0];
    }

    public void DisplayNextSentence()
    {

        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dtext.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dtext.text += letter;
            yield return new WaitForSeconds(0.06f);
        }
    }

    void EndDialogue()
    {
        ani.SetBool("isOpen", false);
        Debug.Log("End of Dialogue");
    }
}
