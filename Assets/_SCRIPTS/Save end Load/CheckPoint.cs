using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator anim;
    public string checkPointID;
    public bool actived;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    [ContextMenu("Generate ID")]
    private void GenerateID()
    {
        checkPointID = System.Guid.NewGuid().ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ActiveCheckPoint();
        }
    }

    public void ActiveCheckPoint()
    {
        actived = true;
        anim.SetBool("isActive", true);
    }
}
