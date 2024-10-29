using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationController : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
