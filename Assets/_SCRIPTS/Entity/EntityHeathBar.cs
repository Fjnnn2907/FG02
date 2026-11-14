using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHeathBar : MonoBehaviour
{
    private RectTransform myTransform;
    private Entity entity;
    private Slider slider;
    private StatManager stat;
    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
        myTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        stat = GetComponentInParent<StatManager>();
    }
    private void OnEnable()
    {
        if (entity != null)
            entity.onFliped += FlipUI;
        if (stat != null)
            stat.onHealthChanged += UpdateHealth;

        UpdateHealth();
    }
    private void Start()
    {
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        if (slider == null || stat == null)
            return;

        slider.maxValue = stat.GetMaxHealthValue();
        slider.value = stat.currentHealth;
    }

    public void FlipUI()
    {
        if (myTransform != null)
            myTransform.Rotate(0, 180, 0);
    }
    private void OnDisable()
    {
        if (entity != null)
            entity.onFliped -= FlipUI;
        if (stat != null)
            stat.onHealthChanged -= UpdateHealth;
    }
}
