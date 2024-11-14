using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintUI : MonoBehaviour
{
    public RectTransform canvas;
    public PlayerController[] players;
    private bool _isShow;
    public bool isShow
    {
        get { return _isShow; }
        set 
        { 
            _isShow = value;
            if (_isShow)
            {
                if(canvas != null)
                {
                    canvas.gameObject.SetActive(true);
                    foreach(var player in players)
                    {
                        player.isMovable = false;
                    }
                }
            }
            else
            {
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(false);
                    foreach (var player in players)
                    {
                        player.isMovable = true;
                    }
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShow = !isShow;
        }
    }
}
