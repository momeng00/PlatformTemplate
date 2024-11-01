using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TempSwitchBlock : Switchable, IReset
{
    public Vector2 respawn;
    // Start is called before the first frame update
    void Start()
    {
        respawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SwitchOn()
    {
        Debug.Log("asd");
    }
    public override void SwitchOff()
    {
        Debug.Log("asddasda");
    }

    public void Reset()
    {
        Debug.Log("hi Die");
        transform.position = respawn;
    }
}
