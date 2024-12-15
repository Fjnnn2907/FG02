using UnityEngine;


public class EvenTriggerAnim : MonoBehaviour
{
    Character character => GetComponentInParent<Character>();
    public void TriggerAnimation()
    {
        character.AnimationTrigger();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyStat targetStat = collision.GetComponent<EnemyStat>();
            character.stats.DoDamage(targetStat);
        }
    }
    public void CreateSword()
    {
        SkillManager.instance.swordSkill.CreateSword();
    }
}
