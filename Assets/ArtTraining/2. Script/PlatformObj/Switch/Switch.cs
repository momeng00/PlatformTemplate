using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IReset
{
    [SerializeField]public Switchable[] block;
    public bool used = false;
    private Sprite originSprite;
    public Sprite turnOn;
    private Vector2 originVector;
    public float moveSpeed;

    public void Use()
    {
        if (!used)
        {
            GameManager.instance.gameObject.GetComponent<AudioManager>().Play("Switch");
            foreach (Switchable switchable in block)
            {
                switchable.SwitchOn();
            }
            used = true;
        }
        else if (used)
        {
            foreach (Switchable switchable in block)
            {
                switchable.SwitchOff();
            }
            used = false;
        }
        
    }
    private void Update()
    {
        
        
    }
    private void Start()
    {
        originSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        originVector = transform.position;
        moveSpeed = 0.01f;
        StartCoroutine("SwitchMove");
    }
    public void DetectedSwitch()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = turnOn;
    }

    public void UnDetectedSwitch()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = originSprite;
    }
    IEnumerator SwitchMove()
    {
        while (true)
        {
            if (transform.position.y > originVector.y + 0.1f)
            {
                moveSpeed = Mathf.Abs(moveSpeed) * -1;
            }
            else if (transform.position.y < originVector.y - 0.1f)
            {
                moveSpeed = Mathf.Abs(moveSpeed);
            }
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed);
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    public void Reset()
    {
        used = false;
    }
}