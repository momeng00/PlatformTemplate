using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    IReset[] resettableObjects;
    private void Start()
    {
        resettableObjects = FindObjectsOfType<MonoBehaviour>().OfType<IReset>().ToArray();
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
            
            foreach (IReset resettable in resettableObjects)
            {
                resettable.Reset(); // Reset() 메서드 호출
            }
        }
    }
}
