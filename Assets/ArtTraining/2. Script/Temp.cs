using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Invoke("NextScene", 0.7f);
        }
    }
    public void NextScene()
    {
        SceneManager.LoadSceneAsync("Openning");
    }
}
