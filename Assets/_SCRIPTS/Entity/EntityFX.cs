using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash Fx")]
    [SerializeField] private Material HitMaterial;
    private Material OriginalMaterial;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        OriginalMaterial = sr.material;
    }
    private IEnumerator HitFlashFx()
    {
        sr.material = HitMaterial;

        yield return new WaitForSeconds(.2f);

        sr.material = OriginalMaterial;
    }
    private void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }
    private void CancelRedBlink()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
