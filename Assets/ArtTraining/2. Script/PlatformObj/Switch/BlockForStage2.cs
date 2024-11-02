using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockState
{
    On,
    Off,
}
public class BlockForStage2 : Switchable, IReset
{
    private Animator ani;
    private BoxCollider2D col;
    public void Reset()
    {
        ChangeState(BlockState.Off);
    }

    // Start is called before the first frame update
    void Start()
    {
        col=GetComponent<BoxCollider2D>();
        ani=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SwitchOn();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SwitchOff();
        }
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
                ani.SetBool("isOn", true);
                col.enabled = true;
                break;
            case BlockState.Off:
                ani.SetBool("isOn",false);
                col.enabled = false;
                break;

        }
    }
}
