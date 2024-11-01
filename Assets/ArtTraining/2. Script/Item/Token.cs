using UnityEngine;

public class Token : Item, IReset
{
    Vector2 respawn;
    public override void GetItem()
    {
        base.GetItem();
    }

    public override void Start()
    {
        base.Start();
        respawn = transform.position;
    }

    public override void Update()
    {
        base.Update();
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GetGamePoint();
            gameObject.SetActive(false);
        }
    }

    public void Reset()
    { 
        gameObject.SetActive(true);
        transform.position = respawn;
        GameManager.instance.ClearGameManager();
    }
}