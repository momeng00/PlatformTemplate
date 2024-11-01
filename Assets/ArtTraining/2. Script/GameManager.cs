using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public float soundVolume;
    public int forGameClear;

    public void ClearGameManager()
    {
        forGameClear = 0;
    }
    public void GetGamePoint()
    {
        forGameClear++;
        Debug.Log("Up");
    }
}
