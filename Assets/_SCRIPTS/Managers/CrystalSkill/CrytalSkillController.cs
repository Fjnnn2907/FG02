using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrytalSkillController : MonoBehaviour
{
    private float crystalDuraction;
    public void SetupScrystal(float _crystalDuraction)
    {
        crystalDuraction = _crystalDuraction;
    }
    private void Update()
    {
        crystalDuraction -= Time.deltaTime;

        if (crystalDuraction < 0)
            SeftDestroy();
    }
    public void SeftDestroy()
    {
        Destroy(gameObject);
    }
}
