using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.tag == "Player")
        {
            //FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            StartCoroutine(Time());
        }
    }
    IEnumerator Time()
    {
        yield return new WaitForSeconds(3f);
        Destroy(transform.gameObject);
    }
}
