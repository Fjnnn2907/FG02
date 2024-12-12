using System.Collections.Generic;
using UnityEngine;

public class BackholeSkillController : MonoBehaviour
{
    [Header("HotKey")]
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keycodeList;

    [Header("Backhole")]
    public float maxSize;
    public float growSpeed;
    public bool canGrow;

    private List<Transform> targetEnemy = new();
    private void Update()
    {
        if (canGrow)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(collision);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (keycodeList.Count <= 0)
            return;

        var newObj = Instantiate(hotKeyPrefab, collision.transform.position +
            new Vector3(0, 2), Quaternion.identity);

        var keycode = keycodeList[Random.Range(0, keycodeList.Count)];
        keycodeList.Remove(keycode);

        var newScirptHotKey = newObj.GetComponent<BackholeHotKeyController>();

        newScirptHotKey.SetupHotKey(keycode, collision.transform, this);
    }

    public void AddListEnemy(Transform _enemy)
    {
        targetEnemy.Add(_enemy);
    }
}
