// This is attached to each note

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupNotesTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    void OnTriggerEnter(Collider col)
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (col.gameObject.transform.tag == "Player")
            {
                GameManager.Instance.pickupnote();
                Destroy(transform.gameObject);
                Debug.Log("you picked up a note");
                //FindObjectOfType<DialogueTrigger>().TriggerDialogue();
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
        }
    }
}
