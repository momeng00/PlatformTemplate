using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : CharacterController
{
    IReset[] resettableObjects;
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
        CheckForWall();
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
    void CheckForWall()
    {
        Vector2 wallTopCastCenter = rb.position + Vector2.up * 0.5f;
        RaycastHit2D topHit = Physics2D.Raycast(wallTopCastCenter, Vector2.right * direction, 0.2f, groundMask);
        if(topHit)
        {
            rb.AddForce(Vector2.right * direction *-2f,ForceMode2D.Impulse);
        }
    }

    protected override void Start()
    {
        base.Start();
        respawn = transform.position;
        isMovable = true;
        resettableObjects = FindObjectsOfType<MonoBehaviour>().OfType<IReset>().ToArray();
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
            // Ÿ�� ����
            Collider2D col
                = Physics2D.OverlapCircle(rb.position + (Vector2.up*0.5f), _switchDetectRange, _switchMask);

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
        foreach (IReset resettable in resettableObjects)
        {
            resettable.Reset(); // Reset() �޼��� ȣ��
        }
    }
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + Vector3.up *0.5f, _switchDetectRange);
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