using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Block : MonoBehaviour
{
    
    public virtual void Start()
    {

    }
    public virtual void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController py = collision.gameObject.GetComponent<PlayerController>();
            py.moveSpeed = 4f;
            if (collision.gameObject.transform.position.y > transform.position.y)
            {
                py.moveSpeed = 8f;
                Use();
            }
        }
    }
    public virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController py = collision.gameObject.GetComponent<PlayerController>();
            py.moveSpeed = 8f;
        }
    }
    public virtual void Use()
    {
        Debug.Log("Use");
    }
}
