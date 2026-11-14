using TMPro;
using UnityEngine;

public class BackholeHotKeyControllerGUI : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode keyCode;
    private TextMeshProUGUI hotkeyText;

    private Transform myTarget;
    private BackholeSkillController backhole;
    public void SetupHotKey(KeyCode _newKeyCode, Transform _enemyTarget, BackholeSkillController _backhole)
    {
        sr = GetComponent<SpriteRenderer>();
        hotkeyText = GetComponentInChildren<TextMeshProUGUI>();

        myTarget = _enemyTarget;
        backhole = _backhole;

        keyCode = _newKeyCode;
        hotkeyText.text = _newKeyCode.ToString();
    }
    private bool used;
    private void Update()
    {
        if (!used && Input.GetKeyDown(keyCode))
        {
            used = true;
            if (backhole != null)
                backhole.AddListEnemy(myTarget);

            if (hotkeyText != null) hotkeyText.color = Color.clear;
            if (sr != null) sr.color = Color.clear;
        }
    }

}
