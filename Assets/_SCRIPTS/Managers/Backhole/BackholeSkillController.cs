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

    private HashSet<Enemy> frozenEnemies = new();
    private bool isFinishing;

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
            targetEnemy.RemoveAll(t => t == null);
            if (targetEnemy.Count > 0)
                CloneAttack();
            else
                FinishBackhole();
        }

        if (Input.GetKeyUp(KeyCode.R) && !canSkrink && !canAttack)
        {
            targetEnemy.RemoveAll(t => t == null);
            if (targetEnemy.Count > 0)
                CloneAttack();
            else
                FinishBackhole();
        }

        if (cloneAttackTimer < 0 && canAttack && amoutAttack > 0)
        {
            cloneAttackTimer = cloneAttackCooldown;
            targetEnemy.RemoveAll(t => t == null);

            if (targetEnemy.Count <= 0)
            {
                amoutAttack = 0;
                Invoke("FinishBackhole", .2f);
            }
            else
            {
                int radomIndex = Random.Range(0, targetEnemy.Count);
                float xOffset = Random.Range(0, 100) > 50 ? 2f : -2f;

                if (SkillManager.instance != null && SkillManager.instance.cloneSkill != null)
                    SkillManager.instance.cloneSkill.CreateClone(targetEnemy[radomIndex], new Vector3(xOffset, 0));

                amoutAttack--;
                if (amoutAttack <= 0)
                {
                    Invoke("FinishBackhole", .5f);
                }
            }
        }

        if (canGrow && !canSkrink)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);

        if (canSkrink)
        {
            float shrinkRate = skrinkSpeed > 0 ? skrinkSpeed : growSpeed;
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkRate * Time.deltaTime);
            if (transform.localScale.x <= 0)
                Destroy(gameObject);
        }
    }

    private void CloneAttack()
    {
        if (targetEnemy.Count <= 0 || canSkrink) return;
        canAttack = true;
        DestroyHotKey();
        canCtrateHotKey = false;

        if (characterCanDisapear)
        {
            characterCanDisapear = false;
            if (PlayerManager.instance != null && PlayerManager.instance.character != null && PlayerManager.instance.character.fx != null)
                PlayerManager.instance.character.fx.TanHinh(true);
        }
    }

    private void FinishBackhole()
    {
        if (isFinishing) return;
        isFinishing = true;

        DestroyHotKey();
        canSkrink = true;
        canAttack = false;
        if (PlayerManager.instance != null && PlayerManager.instance.character != null)
            PlayerManager.instance.character.ExitBackholeSkill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && !frozenEnemies.Contains(enemy))
        {
            frozenEnemies.Add(enemy);
            enemy.FreezeTime(true);

            CreateHotKey(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && frozenEnemies.Remove(enemy))
        {
            enemy.FreezeTime(false);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (keycodeList == null || keycodeList.Count <= 0 || hotKeyPrefab == null)
            return;

        if (!canCtrateHotKey) return;

        var newObj = Instantiate(hotKeyPrefab, collision.transform.position +
            new Vector3(0, 2), Quaternion.identity);

        createHotKey.Add(newObj);

        var keycode = keycodeList[Random.Range(0, keycodeList.Count)];
        keycodeList.Remove(keycode);

        var newScirptHotKey = newObj.GetComponent<BackholeHotKeyControllerGUI>();

        if (newScirptHotKey != null)
            newScirptHotKey.SetupHotKey(keycode, collision.transform, this);
    }

    private void DestroyHotKey()
    {
        if (createHotKey.Count <= 0) return;

        for (int i = 0; i < createHotKey.Count; i++)
        {
            if (createHotKey[i] != null)
                Destroy(createHotKey[i]);
        }
        createHotKey.Clear();
    }

    public void AddListEnemy(Transform _enemy)
    {
        if (_enemy != null && !targetEnemy.Contains(_enemy))
            targetEnemy.Add(_enemy);
    }
}
