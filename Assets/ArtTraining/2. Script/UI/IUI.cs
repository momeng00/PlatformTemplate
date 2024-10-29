using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUI
{
    bool inputEnable { get; set; }
    int sortingOrder { get; set; }
    void Show();
    void Hide();
    void InputAction();
}
