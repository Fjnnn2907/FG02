using UnityEngine;


public class EvenTriggerAnim : MonoBehaviour
{
    private Character cachedCharacter;
    private Character character => cachedCharacter != null ? cachedCharacter : (cachedCharacter = GetComponentInParent<Character>());

    private void Awake()
    {
        cachedCharacter = GetComponentInParent<Character>();
    }

    public void TriggerAnimation()
    {
        if (character != null)
            character.AnimationTrigger();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(2, null);

            EnemyStat targetStat = collision.GetComponent<EnemyStat>();

            if (targetStat != null && character != null && character.stats != null)
            {
                character.stats.DoDamage(targetStat);
                character.stats.DoMagicDamage(targetStat);

                if (Inventory.instance != null)
                {
                    var weaponItem = Inventory.instance.GetItemEquipment(EquipmentType.Vukhi);
                    if (weaponItem != null)
                        weaponItem.ItemEffect(targetStat.transform);
                }
            }
        }
    }
    public void CreateSword()
    {
        if (SkillManager.instance != null && SkillManager.instance.swordSkill != null)
            SkillManager.instance.swordSkill.CreateSword();
    }
}
