using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : Platform
{
    [Header("-는 왼쪽 +는 오른쪽 속도는 크기")]public float move;
    private PlayerController playerController;
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 Player 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.isMovable = false;
                playerController.move = new Vector2(move * playerController.moveSpeed , 0.0f);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 플랫폼에서 플레이어가 떨어질 때
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController != null)
            {
                playerController.rb.AddForce(playerController.move * 0.03f , ForceMode2D.Impulse);
                // 원래 속도로 되돌리거나 다른 이동 방식으로 설정
                playerController.isMovable = true;
            }
            playerController = null; // 플레이어가 플랫폼에서 떠났으므로 참조 해제
        }
    }
}
