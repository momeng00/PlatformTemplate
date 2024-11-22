using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CharacterPlayEnd : MonoBehaviour
{
    public Volume volume;
    private Bloom bloom;
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
        if(volume != null)
        {
            volume.profile.TryGet(out bloom);
        }
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
            StartCoroutine("ZoomOutCoroutine");
            isMovable = false;
            move = Vector2.zero;
            Invoke("ZoomOutEnd",0.78f);
            Debug.Log("접촉");
            //SceneManager.LoadSceneAsync("Ending");
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
        if (horizontal != 0)
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
            if (horizontal >= 0)
            {
                move = new Vector2(horizontal * moveSpeed, 0.0f);
            }
            else
            {
                move = Vector2.zero;
            }
            
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
                addWidth = horizontal > 0 ? 2.5f : 0f;
            }
            foreach (RectTransform v in UIWidth)
            {
                initialWidth = v.sizeDelta.x;
                targetWidth = initialWidth + addWidth;
               
                v.sizeDelta = new Vector2(targetWidth, v.sizeDelta.y);
            }
            yield return new WaitForSeconds(0.15f);
        }
        
    }
    public void ZoomOutEnd()
    {
        SceneManager.LoadSceneAsync("Ending");
    }

    IEnumerator ZoomOutCoroutine()
    {
        float initialHeight;
        float targetHeight;
        float addHeight;
        float initialWidth;
        float targetWidth;
        float addWidth;
        Debug.Log("ZoomOut 진입1");
        for (int i=0; i <=160; i++)
        {
            Debug.Log("ZoomOut 진입2");
            addHeight = -1.5f * 1.5f;
            addWidth = -2.5f * 1.5f;
            foreach (RectTransform v in UIWidth)
            {
                initialWidth = v.sizeDelta.x;
                targetWidth = initialWidth + addWidth;

                v.sizeDelta = new Vector2(targetWidth, v.sizeDelta.y);
            }
            foreach (RectTransform v in UIHeight)
            {
                initialHeight = v.sizeDelta.y;
                targetHeight = initialHeight + addHeight;
                v.sizeDelta = new Vector2(v.sizeDelta.x, targetHeight);
            }
            if (volume != null)
                bloom.intensity.value += 0.08f;
            yield return new WaitForSeconds(0.000625f);
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
                addHeight = horizontal > 0 ? 1.5f : 0f;
            }
            foreach (RectTransform v in UIHeight)
            {
                initialHeight = v.sizeDelta.y;
                targetHeight = initialHeight + addHeight;
                v.sizeDelta = new Vector2(v.sizeDelta.x, targetHeight);
            }
            yield return new WaitForSeconds(0.15f);
        }

    }
}
