// This is attached to each key, and is used to detecting if the player is overalapping the key

using UnityEngine;
using System.Collections;

public class PickupTrigger : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        GameManager.Instance.pickup();
        Destroy(transform.parent.gameObject);
    }
}
