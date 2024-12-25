using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyStat : StatManager
{
    private Enemy enemy;

    [Header("Level")]
    [SerializeField] private int level = 1;

    [Range(0f, 1f)]
    [SerializeField] private float percantedModifier = .4f;

    public Stat moneyDropAmout;
    protected override void Start()
    {
        moneyDropAmout.SetDefaultValue(100);
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

        Modify(moneyDropAmout);
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


        PlayerManager.instance.coint += moneyDropAmout.GetValue();
        enemy.Deah();
    }
}
