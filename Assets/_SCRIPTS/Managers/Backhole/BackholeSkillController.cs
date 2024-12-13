using System.Collections.Generic;
using UnityEngine;


public class BackholeSkillController : MonoBehaviour
{
    [Header("HotKey")]
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keycodeList;

    [Header("Backhole")]
    private float maxSize;
    private float growSpeed;
    public bool canGrow = true;
    private float skrinkSpeed;
    public bool canSkrink;
    private bool canCtrateHotKey = true;
    private float backholeTimer;
    private bool characterCanDisapear = true;
    private int amoutAttack;
    public float cloneAttackCooldown;
    private float cloneAttackTimer;
    private bool canAttack;

    private List<Transform> targetEnemy = new();
    private List<GameObject> createHotKey = new();

    public void SetupBlackhole(float _maxSize, float _growSpeed, float _skrinkSpeed, int _amoutAttack, float _cloneAttackCooldown, float _backholeDuration)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        skrinkSpeed = _skrinkSpeed;
        amoutAttack = _amoutAttack;
        cloneAttackCooldown = _cloneAttackCooldown;
        backholeTimer = _backholeDuration;
    }
    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;
        backholeTimer -= Time.deltaTime;

        if (backholeTimer < 0)
        {
            backholeTimer = Mathf.Infinity;
            if (targetEnemy.Count > 0)
                CloneAttack();
            else
                FinishBackhole();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            CloneAttack();
        }

        if (cloneAttackTimer < 0 && canAttack && amoutAttack > 0)
        {
            cloneAttackTimer = cloneAttackCooldown;

            int radomIndex = Random.Range(0, targetEnemy.Count);
            float xOffset;
            if (Random.Range(0, 100) > 50)
                xOffset = 2;
            else
                xOffset = -2;
            SkillManager.instance.cloneSkill.CreateClone(targetEnemy[radomIndex], new Vector3(xOffset, 0));
            amoutAttack--;
            if (amoutAttack <= 0)
            {
                Invoke("FinishBackhole", .5f);
            }
        }

        if (canGrow && !canSkrink)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);

        if (canSkrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), growSpeed * Time.deltaTime);
            if (transform.localScale.x <= 0)
                Destroy(gameObject);
        }
    }

    private void CloneAttack()
    {
        if (targetEnemy.Count <= 0) return;
        canAttack = true;
        DestroyHotKey();
        canCtrateHotKey = false;

        if (characterCanDisapear)
        {
            characterCanDisapear = false;
            PlayerManager.instance.character.TanHinh(true);
        }
    }

    private void FinishBackhole()
    {
        DestroyHotKey();
        canSkrink = true;
        canAttack = false;
        PlayerManager.instance.character.ExitBackholeSkill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<Enemy>()?.FreezeTime(false);

    private void CreateHotKey(Collider2D collision)
    {
        if (keycodeList.Count <= 0)
            return;

        if (!canCtrateHotKey) return;

        var newObj = Instantiate(hotKeyPrefab, collision.transform.position +
            new Vector3(0, 2), Quaternion.identity);

        createHotKey.Add(newObj);

        var keycode = keycodeList[Random.Range(0, keycodeList.Count)];
        keycodeList.Remove(keycode);

        var newScirptHotKey = newObj.GetComponent<BackholeHotKeyControllerGUI>();

        newScirptHotKey.SetupHotKey(keycode, collision.transform, this);
    }

    private void DestroyHotKey()
    {
        if (createHotKey.Count <= 0) return;

        for (int i = 0; i < createHotKey.Count; i++)
        {
            Destroy(createHotKey[i]);
        }
    }

    public void AddListEnemy(Transform _enemy)
    {
        targetEnemy.Add(_enemy);
    }
}
