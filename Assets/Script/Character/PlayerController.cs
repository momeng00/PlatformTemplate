using UnityEngine;

public class PlayerController : CharacterController
{
    public float jumpForce;
    public bool hasJumped;
    protected override void Start()
    {
        base.Start();
        isMovable = true;
    }

    protected override void Update()
    {
        base.Update();
        if (isMovable)
        {
            horizontal = Input.GetAxis("Horizontal");
            direction = horizontal;
        }

        if (!hasJumped & Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            hasJumped = true;
        }

        if (hasJumped & isGrounded & rb.velocity.y <= 0)
        {
            hasJumped = false;
        }

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}