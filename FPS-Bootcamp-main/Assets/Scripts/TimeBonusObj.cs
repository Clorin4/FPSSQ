using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonusObj : MonoBehaviour
{
    public float bonusTime = 10f; 
    public GameObject pickupEffect; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.sharedInstance.AddTime(bonusTime);
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
