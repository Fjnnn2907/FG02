using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void AnimationTrigger()
    {
        if (enemy != null)
        {
            enemy.AnimationTrigger();
        }
        else
        {
            Debug.LogWarning("Sekeleton component not found in parent.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("character"))
        {
            collision.GetComponent<Character>().Damege();
        }
    }
    protected void OpenCounterAttack()
    {
        enemy.OpenCounterAttack();
    }
    protected void CloseCounterAttack()
    {
        enemy.CloseCounterAttack();
    }
}
