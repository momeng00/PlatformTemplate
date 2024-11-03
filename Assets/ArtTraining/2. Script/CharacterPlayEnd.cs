using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterPlayEnd : MonoBehaviour
{
    public float moveSpeed;
    public bool isMovable;
    public Vector2 move;
    public Rigidbody2D rb;
    public float horizontal;
    public Animator ani;
    public bool isRun;
    public GameObject Player;
    public RectTransform[] UIWidth;
    public RectTransform[] UIHeight;

    private void Start()
    {
        if (isMovable)
        {
            StartCoroutine("ExpandWidthCoroutine");
            StartCoroutine("ExpandHeightCoroutine");
        }
        isRun=false;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "First":
                break;
            case "Second":
                break;
            case "Third":
                isRun = true;
                moveSpeed = 8.0f;
                break;
            case "Fourth":
                isRun = false;
                moveSpeed = 3.0f;
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == Player)
        {
            SceneManager.LoadSceneAsync("Ending");
        }
    }
    public virtual void Move()
    {
        rb.position += move * Time.fixedDeltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal !=0)
        {
            if (isRun)
            {
                ani.SetBool("isWalk", true);
                ani.SetBool("isWalkWalk", false);
            }
            else
            {
                ani.SetBool("isWalkWalk", true);
                ani.SetBool("isWalk", false);
            }
            
        }
        else
        {
            ani.SetBool("isWalkWalk", false);
            ani.SetBool("isWalk", false);
        }
        if (isMovable)
        {
            move = new Vector2(horizontal * moveSpeed, 0.0f);
        }
    }
    private void FixedUpdate()
    {
        Move();
    }


    IEnumerator ExpandWidthCoroutine()
    {
        float initialWidth;
        float targetWidth;
        float addWidth;
        while (true)
        {
            if (horizontal == 0){
                addWidth = 0f;
            }
            else
            {
                addWidth = horizontal > 0 ? 5f : -5f;
            }
            foreach (RectTransform v in UIWidth)
            {
                initialWidth = v.sizeDelta.x;
                targetWidth = initialWidth + addWidth;
               
                v.sizeDelta = new Vector2(targetWidth, v.sizeDelta.y);
            }
            yield return new WaitForSeconds(0.3f);
        }
        
    }
    IEnumerator ExpandHeightCoroutine()
    {
        float initialHeight;
        float targetHeight;
        float addHeight;
        while (true)
        {
            if (horizontal == 0)
            {
                addHeight = 0f;
            }
            else
            {
                addHeight = horizontal > 0 ? 3f : -3f; ;
            }
            foreach (RectTransform v in UIHeight)
            {
                initialHeight = v.sizeDelta.y;
                targetHeight = initialHeight + addHeight;
                v.sizeDelta = new Vector2(v.sizeDelta.x, targetHeight);
            }
            yield return new WaitForSeconds(0.3f);
        }

    }
}
