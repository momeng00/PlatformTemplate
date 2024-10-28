using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]public List<Switchable> block;
    public bool used = false;

    public void Use()
    {
        if (!used)
        {
            foreach (Switchable switchable in block)
            {
                switchable.SwitchOff();
            }
            used = true;
        }
        else if (used)
        {
            foreach (Switchable switchable in block)
            {
                switchable.SwitchOn();
            }
            used = false;
        }
        
    }
}