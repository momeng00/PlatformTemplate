using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempSetting : MonoBehaviour
{
    public List<Button> btn;
    public RectTransform Canvas;
    // Start is called before the first frame update
    void Start()
    {
        Canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Canvas.gameObject.SetActive(false);
    }
}
