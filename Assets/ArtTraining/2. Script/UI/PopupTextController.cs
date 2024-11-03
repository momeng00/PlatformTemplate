using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[Serializable]
public class Data
{
    public int id;
    public string text;
}
public class PopupTextController : MonoBehaviour
{

    [SerializeField]Data[] data;
    Dictionary<int, string> popUp;
    public UIPopUp[] text;
    int ValueMax;
    int curText;
    void Start()
    {
        popUp = new Dictionary<int, string>();
        InputData();
        StartCoroutine(PopUpCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InputData()
    {
        foreach(var data in data)
        {
            popUp.Add(data.id,data.text);
        }
        ValueMax = popUp.Count;
    }

    public void PopUpText(int pos, int value)
    {
        text[pos].PopUp(popUp[value]);
        curText = pos;
    }

    IEnumerator PopUpCoroutine()
    {
        while(true) 
        {
            int randomTextValue = UnityEngine.Random.Range(0, ValueMax);
            int randomTextPos = UnityEngine.Random.Range(0, text.Length);
            PopUpText(randomTextPos, randomTextValue);
            yield return new WaitForSeconds(6f);
        }
        
    }
}
