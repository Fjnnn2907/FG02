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
        AudioManager.instance.PlaySFX(2, null);

        if (collision.CompareTag("Enemy"))
        {
            EnemyStat targetStat = collision.GetComponent<EnemyStat>();

            if(targetStat != null)
                character.stats.DoDamage(targetStat);

            var weaponItem = Inventory.instance.GetItemEquipment(EquipmentType.Vukhi);

            if (weaponItem != null)
                weaponItem.ItemEffect(targetStat.transform);
        }
    }
    public void CreateSword()
    {
        SkillManager.instance.swordSkill.CreateSword();
    }
}
