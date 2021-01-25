// This is attached to each key, and is used to detecting if the player is overalapping the key

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupNotesTrigger : MonoBehaviour
{
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.tag == "Player")
        {
            GameManager.Instance.pickupnote();
            Destroy(transform.gameObject);
            Debug.Log("you picked up a note");
        }
    }
}
