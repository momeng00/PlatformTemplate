using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : Platform
{
    [Header("-�� ���� +�� ������ �ӵ��� ũ��")]public float move;
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
        // �浹�� ������Ʈ�� Player �±׸� ������ �ִ��� Ȯ��
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
        // �÷������� �÷��̾ ������ ��
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController != null)
            {
                playerController.rb.AddForce(playerController.move * 0.03f , ForceMode2D.Impulse);
                // ���� �ӵ��� �ǵ����ų� �ٸ� �̵� ������� ����
                playerController.isMovable = true;
            }
            playerController = null; // �÷��̾ �÷������� �������Ƿ� ���� ����
        }
    }
}
