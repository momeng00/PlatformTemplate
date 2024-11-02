using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            
            GameObject[] gameObject = GameObject.FindGameObjectsWithTag("Player");
            foreach (var p in gameObject)
            {
                PlayerController playerController = p.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    
                    playerController.ReSpawn();
                }
            }
           
        }
    }
}
