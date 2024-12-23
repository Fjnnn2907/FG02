using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiSkillTreeToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDecription;

    public void ShowToolTip(string _name, string _descripttion)
    {
        textName.text = _name;
        textDecription.text = _descripttion;
        gameObject.SetActive(true);
    }
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
