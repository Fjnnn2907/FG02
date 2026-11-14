using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator anim;
    public string checkPointID;
    public bool actived;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    [ContextMenu("Generate ID")]
    private void GenerateID()
    {
        checkPointID = System.Guid.NewGuid().ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActiveCheckPoint();
        }
    }

    public void ActiveCheckPoint(bool silent = false)
    {
        if (!actived && !silent && AudioManager.instance != null)
            AudioManager.instance.PlaySFX(5, transform);

        actived = true;
        if (anim == null)
            anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetBool("isActive", true);
    }
}
