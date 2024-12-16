using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash Fx")]
    [SerializeField] private Material HitMaterial;
    private Material OriginalMaterial;

    [Header("Colors")]
    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] shockColor; 
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        OriginalMaterial = sr.material;
    }
    private IEnumerator HitFlashFx()
    {
        sr.material = HitMaterial;
        Color currentColor = sr.color;
        sr.color = Color.white;
        
        yield return new WaitForSeconds(.2f);
        
        sr.color = currentColor;
        sr.material = OriginalMaterial;
    }
    private void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }
    #region Ignite Color
    private void IgniteColorFx()
    {
        if (sr.color != igniteColor[0])
            sr.color = igniteColor[0];
        else
            sr.color = igniteColor[1];
    }
    public void IgniteFxFor(float _seconds)
    {
        InvokeRepeating("IgniteColorFx",0, .3f);
        Invoke("CancelColorChange", _seconds);
    }
    #endregion

    #region Shock Color
    private void ShockColorFx()
    {
        if (sr.color != shockColor[0])
            sr.color = shockColor[0];
        else
            sr.color = shockColor[1];
    }
    public void ShockFxFor(float _seconds)
    {
        InvokeRepeating("ShockColorFx", 0, .3f);
        Invoke("CancelColorChange", _seconds);
    }
    #endregion

    #region Chill Color
    private void ChillColorFx()
    {
        if (sr.color != chillColor[0])
            sr.color = chillColor[0];
        else
            sr.color = chillColor[1];
    }
    public void ChillFxFor(float _seconds)
    {
        InvokeRepeating("ChillColorFx", 0, .3f);
        Invoke("CancelColorChange", _seconds);
    }
    #endregion
    private void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
