using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [Header("Major stats")]
    public Stat sucManh; // tang dame va chi so chi mang 1%
    public Stat nhanhNhen;// tang ne tranh va chi so chi mang 1%
    public Stat thongMinh;// tang phep va chi so khang phep 3
    public Stat sucSong; // tang mau 1 den 3 diem

    [Header("OffDefensive stats")]
    public Stat damage;
    public Stat tiLeChiMang;
    public Stat satThuongChimang;
    
    [Header("Defensive stats")]
    public Stat MaxHealth;
    public Stat giap;
    public Stat ne;
    public Stat khangAP;

    [Header("Magic Stats")]
    public Stat satThuongLua;
    public Stat satThuongBang;
    public Stat satThuongAnhSang;

    public bool isBong;
    public bool isDongBang;
    public bool isSotAnhSang;

    
    

    [SerializeField] protected int currentHealth;

    protected virtual void Start()
    {
        satThuongChimang.SetDefaultValue(150);
        currentHealth = MaxHealth.GetValue();
    }

    public virtual void DoDamage(StatManager _targetStats)
    {
        if (CheckTargetAvoidAttack(_targetStats)) return;

        int totalDamage = damage.GetValue() + sucManh.GetValue();

        if (CanCrit())
        {
            Debug.Log("Crit hit" + _targetStats.gameObject.name);
            totalDamage = CritDamege(totalDamage);
        }


        totalDamage = CheckTargerArmor(_targetStats, totalDamage);

        //_targetStats.TakeDamage(totalDamage);
        DoMagicDamage(_targetStats);
    }
    public virtual void DoMagicDamage(StatManager _targetStats)
    {
        int _satThuongLua = satThuongLua.GetValue();
        int _satThuongBang = satThuongBang.GetValue();
        int _satThuongAnhSang = satThuongAnhSang.GetValue();

        int totalMagicDamage = _satThuongLua + _satThuongBang + _satThuongAnhSang + thongMinh.GetValue();
        totalMagicDamage -= _targetStats.khangAP.GetValue() + (_targetStats.thongMinh.GetValue() * 3);
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);

        _targetStats.TakeDamage(totalMagicDamage);
        Debug.Log(totalMagicDamage);
    }
    public void AppyAilments( bool _isBong, bool _isDongBang, bool _isSotAnhSang)
    {
        if(isBong || isDongBang || isSotAnhSang)
            return;

        isBong = _isBong;
        isDongBang = _isDongBang;
        isSotAnhSang = _isSotAnhSang;
    }
    private bool CanCrit()
    {
        int totalTileChiMang = tiLeChiMang.GetValue() + nhanhNhen.GetValue();

        if(Random.Range(0,100) <= totalTileChiMang) 
            return true;

        return false;
    }
    private int CritDamege(int _damage)
    {
        float totalSatThuongChiMang = (satThuongChimang.GetValue() + nhanhNhen.GetValue()) * .1f;

        float critDamage = _damage + totalSatThuongChiMang;

        return Mathf.RoundToInt(critDamage);
    }
    private int CheckTargerArmor(StatManager _targetStats, int totalDamage)
    {
        totalDamage -= _targetStats.giap.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool CheckTargetAvoidAttack(StatManager _targetStats)
    {
        int totalNe = _targetStats.ne.GetValue() +
                    _targetStats.nhanhNhen.GetValue();

        if (Random.Range(0, 100) < totalNe)
        {
            Debug.Log("Ne" + _targetStats.gameObject.name);
            return true;
        }
        return false;
    }

    public virtual void TakeDamage(int _damege)
    {
        currentHealth -= _damege;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
            Deah();
    }

    protected virtual void Deah()
    {
        
    }
}
