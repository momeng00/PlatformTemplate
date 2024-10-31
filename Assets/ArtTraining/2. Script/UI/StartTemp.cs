using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTemp : MonoBehaviour
{
    public string sceneName;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Invoke("NextScene", 1.3f);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
