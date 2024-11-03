using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayEnd : MonoBehaviour
{
    public float moveSpeed;
    public bool isMovable;
    public Vector2 move;
    public Rigidbody2D rb;
    public float horizontal;
    public Animator ani;
    [SerializeField]private Collider2D[] Trigger;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var trigger in Trigger) 
        {
            if(collision == trigger)
            {
                Debug.Log(trigger.name);
            }
        }
    }

    public virtual void Move()
    {
        rb.position += move * Time.fixedDeltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal !=0)
        {
            ani.SetBool("isWalkWalk",true);
        }
        else
        {
            ani.SetBool("isWalkWalk", false);
        }
        if (isMovable)
        {
            move = new Vector2(horizontal * moveSpeed, 0.0f);
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
}
