using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrakScreen : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    public void FaceInDrakScreen()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("faceIn");
    }
    public void FaceOutDrakScreen()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("faceOut");
    }
}
