using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public bool flag;
    public Sound sound;
    public AudioSource audioSource;
    public static BGMPlayer instance;
    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            audioSource.clip = sound.clip;
            flag = false;
        }
    }
}
