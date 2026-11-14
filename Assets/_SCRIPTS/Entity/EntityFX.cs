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

    [Header("Particle")]
    [SerializeField] private ParticleSystem bongFx;
    [SerializeField] private ParticleSystem chillFx;
    [SerializeField] private ParticleSystem shockFx;

    private bool isInvisible;
    private Coroutine hitFlashCoroutine;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
            OriginalMaterial = sr.sharedMaterial;
    }
    private void Start()
    {
        if (sr == null)
            sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null && OriginalMaterial == null)
            OriginalMaterial = sr.sharedMaterial;
    }
    public void TriggerHitFlash()
    {
        if (sr == null || !isActiveAndEnabled)
            return;

        if (hitFlashCoroutine != null)
            StopCoroutine(hitFlashCoroutine);

        hitFlashCoroutine = StartCoroutine(HitFlashFx());
    }
    private IEnumerator HitFlashFx()
    {
        if (sr == null)
            yield break;

        if (HitMaterial != null)
            sr.material = HitMaterial;

        if (!isInvisible)
            sr.color = Color.white;

        yield return new WaitForSeconds(.2f);

        if (sr != null)
        {
            sr.material = OriginalMaterial;
            sr.color = isInvisible ? Color.clear : Color.white;
        }
        hitFlashCoroutine = null;
    }
    public void TanHinh(bool _isTanHinh)
    {
        isInvisible = _isTanHinh;
        if (sr == null)
            return;

        sr.color = _isTanHinh ? Color.clear : Color.white;
    }
    private void RedColorBlink()
    {
        if (sr == null || isInvisible)
            return;

        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }
    #region Ignite Color
    private void IgniteColorFx()
    {
        if (sr == null || isInvisible || igniteColor == null || igniteColor.Length < 2)
            return;

        if (sr.color != igniteColor[0])
            sr.color = igniteColor[0];
        else
            sr.color = igniteColor[1];
    }
    public void IgniteFxFor(float _seconds)
    {
        if (bongFx != null)
            bongFx.Play();

        InvokeRepeating("IgniteColorFx", 0, .3f);
        Invoke("CancelColorChange", _seconds);
    }
    #endregion

    #region Shock Color
    private void ShockColorFx()
    {
        if (sr == null || isInvisible || shockColor == null || shockColor.Length < 2)
            return;

        if (sr.color != shockColor[0])
            sr.color = shockColor[0];
        else
            sr.color = shockColor[1];
    }
    public void ShockFxFor(float _seconds)
    {
        if (shockFx != null)
            shockFx.Play();

        InvokeRepeating("ShockColorFx", 0, .15f);
        Invoke("CancelColorChange", _seconds);
    }
    #endregion

    #region Chill Color
    private void ChillColorFx()
    {
        if (sr == null || isInvisible || chillColor == null || chillColor.Length < 2)
            return;

        if (sr.color != chillColor[0])
            sr.color = chillColor[0];
        else
            sr.color = chillColor[1];
    }
    public void ChillFxFor(float _seconds)
    {
        if (chillFx != null)
            chillFx.Play();

        InvokeRepeating("ChillColorFx", 0, .5f);
        Invoke("CancelColorChange", _seconds);
    }
    #endregion
    private void CancelColorChange()
    {
        if (bongFx != null) bongFx.Stop();
        if (chillFx != null) chillFx.Stop();
        if (shockFx != null) shockFx.Stop();

        CancelInvoke();
        if (sr != null)
            sr.color = isInvisible ? Color.clear : Color.white;
    }
}
