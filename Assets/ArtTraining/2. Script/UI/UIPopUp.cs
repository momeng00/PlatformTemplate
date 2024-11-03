using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIPopUp : MonoBehaviour
{
    public TMP_Text text;
    private Animator ani;
    private void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetBool("isPop", false);
    }

    public void PopUp(string value)
    {
        SetText(value);
        ani.SetBool("isPop", true);
        Invoke("UnPop", 3f);
    }

    public void SetText(string value)
    {
        text.text = value;
    }

    public void UnPop()
    {
        text.text = "";
        ani.SetBool("isPop", false);
    }
}
