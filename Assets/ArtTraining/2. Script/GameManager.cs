using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public string nextScene;
    public float soundVolume;
    private int _score;
    public int forGameClear
    {
        get
        {
            return _score;
        }
        set 
        { 
            _score = value;
            if (_score >= 2)
            {
                gameObject.GetComponent<AudioManager>().Play("Clear");
                Invoke("NextScene",0.9f);
            }

        }
    }

    public void ClearGameManager()
    {
        forGameClear = 0;
    }
    public void GetGamePoint()
    {
        forGameClear++;
    }
    public void NextScene()
    {
        ClearGameManager(); 
        SceneManager.LoadSceneAsync(nextScene);
    }
}
