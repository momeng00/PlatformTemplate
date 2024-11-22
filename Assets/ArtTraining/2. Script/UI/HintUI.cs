using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintUI : MonoBehaviour
{
    public RectTransform canvas;
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
                }
            }
            else
            {
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(false);
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isShow = false;
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
