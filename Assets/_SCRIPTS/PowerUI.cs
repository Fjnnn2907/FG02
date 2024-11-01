using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour
{
    [SerializeField] private Character character;
    public Image PowerImage;

    private void Update()
    {
        PowerImage.fillAmount = Mathf.Clamp01(1 - (character.ver2Timer / 10));
    }
}
