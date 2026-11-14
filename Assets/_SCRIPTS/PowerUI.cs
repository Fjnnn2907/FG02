using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour
{
    [SerializeField] private Character character;
    public Image PowerImage;

    private void Start()
    {
        if (character == null && PlayerManager.instance != null)
            character = PlayerManager.instance.character;
    }

    private void Update()
    {
        if (PowerImage == null || character == null)
            return;

        PowerImage.fillAmount = character.isVer2 ? Mathf.Clamp01(character.ver2Timer / 10f) : 1f;
    }
}
