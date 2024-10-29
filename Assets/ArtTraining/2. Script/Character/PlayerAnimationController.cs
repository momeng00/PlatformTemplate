using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    Idle,
    Jump,
    Walk,
    Die
}
public class PlayerAnimationController : AnimationController
{
    PlayerController player;
    PlayerState _state;
    PlayerState state
    {
        get => _state;
        set
        {
            _state = value;
            switch (value)
            {
                case PlayerState.Idle:
                    animator.SetBool("isWalk",false);
                    break;
                case PlayerState.Jump:
                    animator.SetBool("isJump", true);
                    break;
                case PlayerState.Walk:
                    animator.SetBool("isWalk", true);
                    break;
                case PlayerState.Die:
                    break;
                default:
                    break;
            }
        }
    }
    public override void Start()
    {
        base.Start();
        player = GetComponent<PlayerController>();
    }

    public override void Update()
    {
        base.Update();
        if (state == PlayerState.Jump && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetBool("isJump", false);
        }
        if (player.hasJumped)
        {
            state = PlayerState.Jump;
        }
        else
        {
            if(player.horizontal != 0){
                state = PlayerState.Walk;
            }
            else
            {
                state = PlayerState.Idle;
            }

        }
    }
}
