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

    public Stat moneyDropAmout;
    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
    }
    protected override void Start()
    {
        moneyDropAmout.SetDefaultValue(100);
        SetUpModify();
        base.Start();

        if (enemy == null)
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
        Modify(ne);
        Modify(khangAP);

        Modify(moneyDropAmout);
    }

    public void Modify(Stat _stat)
    {
        int baseVal = _stat.GetBaseValue();
        for (int i = 1; i < level; i++)
        {
            float modifer = baseVal * percantedModifier;

            _stat.AddMotdifier(Mathf.RoundToInt(modifer));
        }
    }

    public override void TakeDamage(int _damege)
    {
        base.TakeDamage(_damege);
    }
    protected override void Deah()
    {
        base.Deah();

        if (PlayerManager.instance != null)
            PlayerManager.instance.coint += moneyDropAmout.GetValue();

        if (enemy != null)
            enemy.Deah();
    }
}
