using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : CharacterController
{
    public float jumpForce;
    public bool hasJumped;
    private Vector2 expectPos;
    [SerializeField]private Switch _switch;
    [SerializeField]private float _switchDetectRange;
    [SerializeField]private LayerMask _switchMask;
    public Vector2 respawn;
    public event Action OnDie;
    public override void Move()
    {   
        base.Move();
        /*
        if (isMovable && !hasJumped)
        {
            expectPos = (Vector2)transform.position + move * 0.1f;
            Debug.DrawLine(expectPos, expectPos + Vector2.down);
            
            if(!Physics2D.Raycast(expectPos + _groundDetectOffset,
                                          Vector2.down,
                                          1f,
                                          groundMask))
            {
                return;
            }
        }
        rb.position += move * Time.fixedDeltaTime;
        */
    }
    protected override void Start()
    {
        base.Start();
        respawn = transform.position;
        isMovable = true;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReSpawn();
        }
        if (isMovable)
        {
            horizontal = Input.GetAxis("Horizontal");
            direction = horizontal;
            if (!hasJumped & Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                hasJumped = true;
            }
        }

        if (hasJumped & isGrounded & rb.velocity.y <= 0)
        {
            hasJumped = false;
        }

        if (_switch && Input.GetKeyDown(KeyCode.E))
        {
            _switch.Use();
        }
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_switch == null)
        {
            // Å¸°Ù °¨Áö
            Collider2D col
                = Physics2D.OverlapCircle(rb.position, _switchDetectRange, _switchMask);

            if (col) { 
                Switch isSwitch = col.GetComponent<Switch>();
                if (isSwitch != null)
                {
                    _switch = isSwitch;
                }
            }
        }
        if (_switch != null && Vector2.Distance(transform.position, _switch.transform.position) > _switchDetectRange)
        {
            _switch = null;
            return;
        }
    }

    public void ReSpawn()
    {
        isMovable = false;
        horizontal = 0;
        Stop();
        rb.isKinematic = true;
        OnDie?.Invoke();
    }
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _switchDetectRange);
    }
    public void BlinkPlayer()
    {
        StartCoroutine(BlinkChracter());
    }

    public IEnumerator BlinkChracter()
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        bool blinkSwitch = true;
        for (int i = 0; i <= 6; i++)
        {
            if (blinkSwitch)
            {
                blinkSwitch = !blinkSwitch;
                color.a = 0.35f;
                gameObject.GetComponent<SpriteRenderer>().color = color;
            }
            else
            {
                blinkSwitch = !blinkSwitch;
                color.a = 0.7f;
                gameObject.GetComponent<SpriteRenderer>().color = color;
            }
            yield return new WaitForSeconds(0.1f);
        }
        isMovable = true;
        rb.isKinematic = false;
        color.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = color;
        yield return null;
    }
}