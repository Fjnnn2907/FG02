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
            if (collision.GetComponent<Enemy>() == null) return;
            collision.GetComponent<Enemy>().Damege();
        }
    }
}
