using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTemp : MonoBehaviour
{
    public string sceneName;
    bool skip;

    private void Start()
    {
        Invoke("NextScene", 40f);
        skip = false;
    }
    public void NextScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    private void Update()
    {
        if(skip & Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            skip= true;
        }
    }
}
