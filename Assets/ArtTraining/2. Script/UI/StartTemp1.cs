using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTemp1 : MonoBehaviour
{
    public string sceneName;

    private void Start()
    {

    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Invoke("NextScene", 1f);
        }
    }
    public void NextScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
