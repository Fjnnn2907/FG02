using TMPro;
using UnityEngine;

public class BackholeHotKeyController : MonoBehaviour
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
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            backhole.AddListEnemy(myTarget);

            hotkeyText.color = Color.clear;
            sr.color = Color.clear;
        }
    }

}
