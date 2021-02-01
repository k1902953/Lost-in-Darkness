using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBattery : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.tag == "Player")
        {
            if (Input.GetKey(KeyCode.C))
            {
                GameManager.Instance.pickupbatt();
                Destroy(transform.gameObject);
                Debug.Log("you picked up a battery");
            }
        }
    }
}
