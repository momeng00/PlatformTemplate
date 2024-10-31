using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{

    #region Ground Detection
    public bool isGrounded
    {
        get
        {
            ground = Physics2D.OverlapBox(rb.position + _groundDetectOffset,
                                          _groundDetectSize,
                                          0.0f,
                                          groundMask);
            return ground;
        }
    }

    public bool isGroundBelowExist
    {
        get
        {
            Vector3 castStartPos = transform.position + (Vector3)_groundDetectOffset + Vector3.down * _groundDetectSize.y + Vector3.down * 0.01f;
            RaycastHit2D[] hits =
                Physics2D.BoxCastAll(origin: castStartPos,
                                     size: _groundDetectSize,
                                     angle: 0.0f,
                                     direction: Vector2.down,
                                     distance: _groundBelowDetectDistance,
                                     layerMask: groundMask);

            RaycastHit2D hit = default;
            if (hits.Length > 0)
                hit = hits.FirstOrDefault(x => ground ?? x != ground);

            groundBelow = hit.collider;
            return groundBelow;
        }
    }

    public Collider2D ground;
    public Collider2D groundBelow;
    [SerializeField] protected Vector2 _groundDetectOffset;
    [SerializeField] protected Vector2 _groundDetectSize;
    [SerializeField] protected float _groundBelowDetectDistance;
    [SerializeField] protected LayerMask groundMask;
    #endregion Ground Detection
    private float _direction;
    public float direction
    {
        get
        {
            return _direction;
        }
        set
        {
            if (value == _direction)
                return;

            if (value < 0)
            {
                _direction = -1;
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else if(value > 0)
            {
                _direction = 1;
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                _direction = value;
            }
        }
    }
    public float moveSpeed;
    public bool isMovable;
    public Vector2 move;
    public Rigidbody2D rb;
    public float horizontal;

    public virtual void Move()
    {
        if (isMovable)
        {
            rb.position += move * Time.fixedDeltaTime;
        }
    }
    public void Stop()
    {
        move = Vector2.zero; 
        rb.velocity = Vector2.zero;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isMovable)
        {
            move = new Vector2(horizontal * moveSpeed, 0.0f);
        }
    }
    protected virtual void FixedUpdate()
    {
        Move();
    }
    protected virtual void OnDrawGizmosSelected()
    {
        DrawGroundDetectGizmos();
        DrawGroundBelowDetectGizmos();
    }
    private void DrawGroundDetectGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)_groundDetectOffset, _groundDetectSize);
    }
    private void DrawGroundBelowDetectGizmos()
    {
        Vector3 castStartPos = transform.position + (Vector3)_groundDetectOffset + Vector3.down * _groundDetectSize.y + Vector3.down * 0.01f;
        RaycastHit2D[] hits =
            Physics2D.BoxCastAll(origin: castStartPos,
                                 size: _groundDetectSize,
                                 angle: 0.0f,
                                 direction: Vector2.down,
                                 distance: _groundBelowDetectDistance,
                                 layerMask: groundMask);

        RaycastHit2D hit = default;
        if (hits.Length > 0)
            hit = hits.FirstOrDefault(x => ground ?? x != ground);


        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(castStartPos, _groundDetectSize);
        Gizmos.DrawWireCube(castStartPos + Vector3.down * _groundBelowDetectDistance, _groundDetectSize);
        Gizmos.DrawLine(castStartPos + Vector3.left * _groundDetectSize.x / 2.0f,
                        castStartPos + Vector3.left * _groundDetectSize.x / 2.0f + Vector3.down * _groundBelowDetectDistance);
        Gizmos.DrawLine(castStartPos + Vector3.right * _groundDetectSize.x / 2.0f,
                        castStartPos + Vector3.right * _groundDetectSize.x / 2.0f + Vector3.down * _groundBelowDetectDistance);

        if (hit.collider != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(castStartPos, _groundDetectSize);
            Gizmos.DrawWireCube(castStartPos + Vector3.down * hit.distance, _groundDetectSize);
            Gizmos.DrawLine(castStartPos + Vector3.left * _groundDetectSize.x / 2.0f,
                            castStartPos + Vector3.left * _groundDetectSize.x / 2.0f + Vector3.down * hit.distance);
            Gizmos.DrawLine(castStartPos + Vector3.right * _groundDetectSize.x / 2.0f,
                            castStartPos + Vector3.right * _groundDetectSize.x / 2.0f + Vector3.down * hit.distance);
        }
    }
}
