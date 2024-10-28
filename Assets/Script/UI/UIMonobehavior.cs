using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIMonobehavior : MonoBehaviour, IUI
{
    private bool _inputEnable;
    public bool inputEnable { get => _inputEnable; set => _inputEnable = value; }
    private int _sortingOrder;
    public int sortingOrder { get => _sortingOrder; set => _sortingOrder = value; }
    public Canvas canvas;
    public event Action onShow;
    public event Action onHide;
    public void Hide()
    {
        gameObject.SetActive(false);
        onShow?.Invoke();
    }
    public void Show()
    {
        gameObject.SetActive(false);
        onHide?.Invoke();
    }
    public virtual void InputAction()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
