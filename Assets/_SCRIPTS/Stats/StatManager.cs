using System.Collections;
using UnityEngine;

public enum BuffType
{
    Dame,
    STLua,
    STBang,
    STDien,
    Giap,
    Ne,
    DeffAP,
    MaxHP
}


public class StatManager : MonoBehaviour
{
    private EntityFX entityFX;

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
    public Stat satThuongDien;

    public bool isBong;
    public bool isDongBang;
    public bool isSocDien;

    private int satThuongBong = 1;

    public float bongTimer;
    private float bongCoolDown = .3f;
    private float satThuongBongTimer;

    private float bangTimer;
    private float bangCoolDown;
    private float socsatThuongBangTimer;

    private float socDienTimer;
    private float thoiGianSetRoi;
    public GameObject tiaSetPreFab;
    public int currentHealth;

    public System.Action onHealthChanged;
    protected bool isDeah;
    protected virtual void Start()
    {
        satThuongChimang.SetDefaultValue(150);
        currentHealth = GetMaxHealthValue();
        entityFX = GetComponent<EntityFX>();
    }
    protected virtual void Update()
    {
        bongTimer -= Time.deltaTime;
        satThuongBongTimer -= Time.deltaTime;
        bangTimer -= Time.deltaTime;
        socDienTimer -= Time.deltaTime;
        thoiGianSetRoi -= Time.deltaTime;

        if (bongTimer < 0)
            isBong = false;

        if (bangTimer < 0)
            isDongBang = false;

        if (socDienTimer < 0)
            isSocDien = false;

        if (satThuongBongTimer < 0 && isBong)
        {
            Debug.Log("thieu rui");
            DecreaseHealthBy(satThuongBong);
            satThuongBongTimer = bongCoolDown;
        }
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

        _targetStats.TakeDamage(totalDamage);
        //DoMagicDamage(_targetStats);
    }
    public virtual void DoMagicDamage(StatManager _targetStats)
    {
        int _satThuongLua = satThuongLua.GetValue();
        int _satThuongBang = satThuongBang.GetValue();
        int _satThuongSet = satThuongDien.GetValue();

        int totalMagicDamage = _satThuongLua + _satThuongBang + _satThuongSet + thongMinh.GetValue();
        totalMagicDamage -= _targetStats.khangAP.GetValue() + (_targetStats.thongMinh.GetValue() * 3);
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);

        _targetStats.TakeDamage(totalMagicDamage);
        Debug.Log(totalMagicDamage);


        if(Mathf.Max(_satThuongLua,_satThuongBang,_satThuongSet) <= 0)
            return;

        bool canThieuRui = _satThuongLua > _satThuongBang && _satThuongLua > _satThuongSet;
        bool canDongBang = _satThuongBang > _satThuongLua && _satThuongBang > _satThuongSet;
        bool canSet = _satThuongSet > _satThuongLua && _satThuongSet > _satThuongBang;

        while(!canThieuRui && !canDongBang && !canSet)
        {
            if(Random.value < .35f && _satThuongLua > 0)
            {
                canThieuRui = true;
                SuDungNguyenTo(canThieuRui, canDongBang, canSet, _targetStats);
                return;
            }

            if (Random.value < .35f && _satThuongBang > 0)
            {
                canDongBang = true;
                SuDungNguyenTo(canThieuRui, canDongBang, canSet, _targetStats);
                return;
            }

            if (Random.value < .35f && _satThuongSet > 0)
            {
                canSet = true;
                SuDungNguyenTo(canThieuRui, canDongBang, canSet, _targetStats);
                return;
            }

        }

        if (canThieuRui)
            _targetStats.setUpSatThuongBong(Mathf.RoundToInt(_satThuongLua * .2f));
        //Debug.Log(Mathf.RoundToInt(_satThuongLua * .2f));
        
        _targetStats.SuDungNguyenTo(canThieuRui, canDongBang,canSet, _targetStats); 

    }
    public void SuDungNguyenTo(bool _isBong, bool _isDongBang, bool _isSocDien, StatManager _targetStats)
    {
        if (isBong || isDongBang || isSocDien)
            return;

        if (_isBong)
        {
            isBong = true;
            bongTimer = 2;
            
            entityFX.IgniteFxFor(2);
        }
        if (_isDongBang)
        {
            isDongBang = true;
            bangTimer = 2;

            GetComponent<Entity>().SlowEntity(.2f,2);
            entityFX.ChillFxFor(2);
        }

        if (_isSocDien)
        {
            isSocDien = true;
            socDienTimer = 2;

            entityFX.ShockFxFor(2);
        }

    }

    private void TiaSet(StatManager _targetStats)
    {
        GameObject PreFab = Instantiate(tiaSetPreFab, transform.position, Quaternion.identity);
        PreFab.GetComponent<TiaSetController>().SetUpTiaSet(satThuongDien.GetValue(), _targetStats);
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
        if(_targetStats.isDongBang)
            totalDamage -= Mathf.RoundToInt(_targetStats.giap.GetValue() * .8f);
        else
            totalDamage -= _targetStats.giap.GetValue();

        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool CheckTargetAvoidAttack(StatManager _targetStats)
    {
        int totalNe = _targetStats.ne.GetValue() +
                    _targetStats.nhanhNhen.GetValue();

        if (isSocDien)
            totalNe += 20;


        if (Random.Range(0, 100) < totalNe)
        {
            Debug.Log("Ne" + _targetStats.gameObject.name);
            return true;
        }
        return false;
    }

    public virtual void TakeDamage(int _damege)
    {
        DecreaseHealthBy(_damege);
        //Debug.Log(currentHealth);

        GetComponent<Entity>().DamageEffect();

        if (currentHealth <= 0 && !isDeah)
        {
            Deah();
            isDeah = true;
        }
    }
    public virtual void BuffStats(int _modifier, float _duration, Stat _statModify)
    {
        StartCoroutine(BuffDuraction(_modifier, _duration, _statModify));
    }

    private IEnumerator BuffDuraction(int _modifier, float _duration, Stat _statModify)
    {
        _statModify.AddMotdifier(_modifier);    

        yield return new WaitForSeconds(_duration);

        _statModify.RemoveMotdifier(_modifier);
    }
    public virtual void HoiPhucMau(int amout)
    {
        currentHealth += amout;

        if(currentHealth > GetMaxHealthValue())
            currentHealth = GetMaxHealthValue();

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void DecreaseHealthBy(int _damege)
    {
        currentHealth -= _damege;

        if (onHealthChanged != null)
            onHealthChanged();
    }
    public void setUpSatThuongBong(int _damege) => satThuongBong = _damege;

    protected virtual void Deah()
    {
        
    }
    
    public int GetMaxHealthValue()
    {
        return MaxHealth.GetValue() + sucSong.GetValue() * 5;
    }

    public Stat StatToModify(BuffType _type)
    {
        if (_type == BuffType.Dame) return damage;
        else if (_type == BuffType.STLua) return satThuongLua;
        else if (_type == BuffType.STBang) return satThuongBang;
        else if(_type == BuffType.STDien) return satThuongDien;
        else if (_type == BuffType.Giap) return giap;
        else if(_type == BuffType.Ne) return ne;
        else if(_type == BuffType.MaxHP) return MaxHealth;
        else if(_type == BuffType.DeffAP) return khangAP;

        return null;
    }
}
