using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTemp : MonoBehaviour
{
    public string sceneName;


    private void Start()
    {
        Invoke("NextScene", 40f);
    }
    public void NextScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
