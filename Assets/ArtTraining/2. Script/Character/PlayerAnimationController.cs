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
    private PlayerAnimationController _instance;
    public PlayerAnimationController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = this;
            }
            return _instance;
        }
    }
    private Camera _camera;
    PlayerController player;
    [SerializeField]PlayerState _state;
    [SerializeField]PlayerState nextState;
    bool isDirty = false;
    PlayerState state
    {
        get => _state;
        set
        {
            if(_state == value)
            {
                return;
            }
            _state = value;
            switch (value)
            {
                case PlayerState.Idle:
                    animator.SetBool("isJump", false);
                    animator.SetBool("isWalk",false);
                    break;
                case PlayerState.Jump:
                    animator.SetBool("isJump", true);
                    break;
                case PlayerState.Walk:
                    animator.SetBool("isJump", false);
                    animator.SetBool("isWalk", true);
                    break;
                case PlayerState.Die:
                    animator.Play("Die");
                    break;
                default:
                    break;
            }
        }
    }
    public override void Start()
    {
        base.Start();
        _camera = Camera.main;
        player = GetComponent<PlayerController>();
        player.OnDie += Die;
    }

    public override void Update()
    {
        if (isDirty)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                isDirty = false;
                player.transform.position=player.respawn;
                player.BlinkPlayer();
                
            }
            return;
        }
        base.Update();
        if (!player.isMovable)
        {
            return;
        }
        if (player.hasJumped)
        {
            nextState = PlayerState.Jump;
        }
        else
        {
            if (player.horizontal != 0){
                nextState = PlayerState.Walk;
            }
            else
            {
                nextState = PlayerState.Idle;
            }
        }
        EnterState(nextState);
    }
    public void EnterState(PlayerState state)
    {
        /* if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        { }*/
        this.state = state;

    }
    public void Die()
    {
        isDirty = true;
        state = PlayerState.Idle;
        EnterState(PlayerState.Die);
        StartCoroutine(CameraEffect());  
    }
    IEnumerator CameraEffect()
    {
        for(int i = 0; i <= 4; i++)
        {
            _camera.transform.position += Vector3.right*0.1f;
            yield return new WaitForSeconds(0.07f);
            _camera.transform.position += Vector3.left * 0.1f;
            _camera.transform.position += Vector3.left * 0.1f;
            yield return new WaitForSeconds(0.07f);
            _camera.transform.position += Vector3.right * 0.1f;
        }
        yield return null;
    }
}
