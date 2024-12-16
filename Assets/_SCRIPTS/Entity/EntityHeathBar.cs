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
    private void Start()
    {
        entity = GetComponentInParent<Entity>();
        myTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        stat = GetComponentInParent<StatManager>();

        entity.onFliped += FlipUI;
        stat.onHealthChanged += UpdateHealth;

        UpdateHealth();
    }
    private void Update()
    {
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        slider.maxValue = stat.GetMaxHealthValue();
        slider.value = stat.currentHealth;
    }

    public void FlipUI()
    {
        myTransform.Rotate(0,180,0);
    }
    private void OnDisable()
    {
        entity.onFliped -= FlipUI;
        stat.onHealthChanged -= UpdateHealth;
    }
}
