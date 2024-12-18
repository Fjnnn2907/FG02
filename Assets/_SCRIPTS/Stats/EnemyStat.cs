using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : StatManager
{
    private Enemy enemy;

    [Header("Level")]
    [SerializeField] private int level = 1;

    [Range(0f, 1f)]
    [SerializeField] private float percantedModifier = .4f;
    protected override void Start()
    {
        SetUpModify();

        base.Start();

        enemy = GetComponent<Enemy>();

    }

    private void SetUpModify()
    {
        Modify(sucManh);
        Modify(nhanhNhen);
        Modify(thongMinh);
        Modify(sucSong);


        Modify(damage);
        Modify(tiLeChiMang);
        Modify(satThuongChimang);


        Modify(MaxHealth);
        Modify(giap);
        Modify(giap);
        Modify(ne);
        Modify(khangAP);
    }

    public void Modify(Stat _stat)
    {
        for(int i = 1; i <= level; i++)
        {
            float modifer = _stat.GetValue() + percantedModifier;

            _stat.AddMotdifier(Mathf.RoundToInt(modifer));
        }
    }

    public override void TakeDamage(int _damege)
    {
        base.TakeDamage(_damege);

        enemy.DamageEffect();
    }
    protected override void Deah()
    {
        base.Deah();

        enemy.Deah();
    }
}
