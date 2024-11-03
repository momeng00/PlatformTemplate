using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public RectTransform transition;
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
                transition.GetComponent<Animator>().Play("Transition_Fadein");
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
        if( BGMPlayer.instance != null && nextScene == "GameJam_Stage_End")
        {
            BGMPlayer.instance.flag = true;
        }
        SceneManager.LoadSceneAsync(nextScene);
    }
    private void Start()
    {
        
        transition.GetComponent<Animator>().Play("Transition_Fadeout");
    }
}
