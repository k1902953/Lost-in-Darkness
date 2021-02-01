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
    public GameObject start;
    public GameObject page;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        //FindObjectOfType<DialogueTrigger>().TriggerDialogue(1);
    }

    public void StartDialogue(Dialogue dialogue, int go)
    {
        ani.SetBool("isOpen", true);
        sentences.Clear();
        if (go == 1)
        {
            foreach (string sentence in dialogue.sentences)
            {
                Debug.Log(gameObject);
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }
        if (go == 2)
        {
            foreach (string sentence in dialogue.sentences)
            {
                Debug.Log(gameObject);
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {

        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        //if (Input.GetKey(KeyCode.X))
        //{
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //}
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
