using UnityEngine;

public class Platform : Switchable
{
    public override void SwitchOff()
    {
        transform.gameObject.SetActive(false);
    }

    public override void SwitchOn()
    {
        transform.gameObject.SetActive(true);
    }
}