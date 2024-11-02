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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GetGamePoint();
            GameManager.instance.gameObject.GetComponent<AudioManager>().Play("Token");
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