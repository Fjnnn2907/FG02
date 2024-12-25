using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrakScreen : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FaceInDrakScreen()
    {
        anim.SetTrigger("faceIn");
    }
    public void FaceOutDrakScreen()
    {
        anim.SetTrigger("faceOut");
    }
}
