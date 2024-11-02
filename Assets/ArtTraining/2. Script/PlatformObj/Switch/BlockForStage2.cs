using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public enum BlockState
{
    On,
    Off,
}
public class BlockForStage2 : Switchable, IReset
{
    private Animator ani;
    public TMP_Text text;
    public Vector2 textPos;
    public string textValue;
    private BoxCollider2D col;
    bool isWork;
    public void Reset()
    {
        ChangeState(BlockState.Off);
    }

    // Start is called before the first frame update
    void Start()
    {
        isWork = false;
        col =GetComponent<BoxCollider2D>();
        ani=GetComponent<Animator>();
        ChangeState(BlockState.Off);
        if (text != null)
        {
            text.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(textPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SwitchOn()
    {
        base.SwitchOn();
        ChangeState(BlockState.On);
    }
    public override void SwitchOff()
    {
        base.SwitchOff();
        ChangeState(BlockState.Off);
    }
    public void ChangeState(BlockState state)
    {
        switch (state)
        {
            case BlockState.On:
                if (text != null)
                {
                    text.text = "";
                    StartCoroutine(Typing());
                }
                ani.SetBool("isOn", true);
                col.enabled = true;
                break;
            case BlockState.Off:
                if (text != null)
                {
                    isWork = false;
                    text.text = "";
                }
                ani.SetBool("isOn",false);
                col.enabled = false;
                break;

        }
    }

    IEnumerator Typing()
    {
        if (!isWork) {
            isWork= true;
            yield return new WaitForSeconds(0.7f);
            while (isWork)
            {
                foreach (char c in textValue.ToCharArray())
                {
                    if (!isWork)
                    {
                        break;
                    }
                    text.text += c;
                    yield return new WaitForSeconds(0.2f);
                }
                isWork = false;
            }
        }
        yield return null;
    }
}
