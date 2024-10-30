using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenTriggerAnim : MonoBehaviour
{
    Character character => GetComponentInParent<Character>();
    public void TriggerAnimation()
    {
        character.AnimationTrigger();
    }
}
