using System.Collections;
using System.Collections.Generic;
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
    public bool isDeah;
    private bool isXuyenGiap;
    private Coroutine xuyenGiapCoroutine;
    protected virtual void Awake()
    {
        entityFX = GetComponent<EntityFX>();
    }
    protected virtual void Start()
    {
        satThuongChimang.SetDefaultValue(150);
        currentHealth = GetMaxHealthValue();
        if (entityFX == null)
            entityFX = GetComponent<EntityFX>();
        onHealthChanged?.Invoke();
    }
    protected virtual void Update()
    {
        if (bongTimer > -1f)
            bongTimer -= Time.deltaTime;
        if (satThuongBongTimer > -1f)
            satThuongBongTimer -= Time.deltaTime;
        if (bangTimer > -1f)
            bangTimer -= Time.deltaTime;
        if (socDienTimer > -1f)
            socDienTimer -= Time.deltaTime;
        if (thoiGianSetRoi > -1f)
            thoiGianSetRoi -= Time.deltaTime;

        if (bongTimer < 0)
            isBong = false;

        if (bangTimer < 0)
            isDongBang = false;

        if (socDienTimer < 0)
            isSocDien = false;

        if (satThuongBongTimer < 0 && isBong && !isDeah)
        {
            DecreaseHealthBy(satThuongBong);
            satThuongBongTimer = bongCoolDown;

            if (currentHealth <= 0 && !isDeah)
            {
                isDeah = true;
                Deah();
            }
        }
    }

    public void LayXuyenGiap(float _duraction)
    {
        if (xuyenGiapCoroutine != null)
            StopCoroutine(xuyenGiapCoroutine);

        xuyenGiapCoroutine = StartCoroutine(XuyenGiap(_duraction));
    }

    private IEnumerator XuyenGiap(float _duraction)
    {
        isXuyenGiap = true;

        yield return new WaitForSeconds(_duraction);

        isXuyenGiap = false;
        xuyenGiapCoroutine = null;
    }

    public virtual void DoDamage(StatManager _targetStats)
    {
        if (_targetStats == null || _targetStats.isDeah)
            return;

        if (CheckTargetAvoidAttack(_targetStats)) return;

        int totalDamage = damage.GetValue() + sucManh.GetValue();

        if (CanCrit())
        {
            totalDamage = CritDamege(totalDamage);
        }

        totalDamage = CheckTargerArmor(_targetStats, totalDamage);

        _targetStats.TakeDamage(totalDamage);
    }
    public virtual void DoMagicDamage(StatManager _targetStats)
    {
        if (_targetStats == null || _targetStats.isDeah)
            return;

        int _satThuongLua = satThuongLua.GetValue();
        int _satThuongBang = satThuongBang.GetValue();
        int _satThuongSet = satThuongDien.GetValue();

        if (_satThuongLua <= 0 && _satThuongBang <= 0 && _satThuongSet <= 0 && thongMinh.GetValue() <= 0)
            return;

        int totalMagicDamage = _satThuongLua + _satThuongBang + _satThuongSet + thongMinh.GetValue();
        totalMagicDamage -= _targetStats.khangAP.GetValue() + (_targetStats.thongMinh.GetValue() * 3);
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);

        if (totalMagicDamage > 0)
            _targetStats.TakeDamage(totalMagicDamage);

        if (Mathf.Max(_satThuongLua, _satThuongBang, _satThuongSet) <= 0 || _targetStats.isDeah)
            return;

        bool canThieuRui = _satThuongLua > _satThuongBang && _satThuongLua > _satThuongSet;
        bool canDongBang = _satThuongBang > _satThuongLua && _satThuongBang > _satThuongSet;
        bool canSet = _satThuongSet > _satThuongLua && _satThuongSet > _satThuongBang;

        if (!canThieuRui && !canDongBang && !canSet)
        {
            List<int> validTypes = new List<int>();
            if (_satThuongLua > 0) validTypes.Add(0);
            if (_satThuongBang > 0) validTypes.Add(1);
            if (_satThuongSet > 0) validTypes.Add(2);

            if (validTypes.Count > 0 && Random.value < 0.35f)
            {
                int chosen = validTypes[Random.Range(0, validTypes.Count)];
                if (chosen == 0) canThieuRui = true;
                else if (chosen == 1) canDongBang = true;
                else if (chosen == 2) canSet = true;
            }
        }

        if (canThieuRui)
            _targetStats.setUpSatThuongBong(Mathf.Max(1, Mathf.RoundToInt(_satThuongLua * .2f)));

        if (canSet && tiaSetPreFab != null)
            TiaSet(_targetStats);

        _targetStats.SuDungNguyenTo(canThieuRui, canDongBang, canSet, _targetStats);
    }
    public void SuDungNguyenTo(bool _isBong, bool _isDongBang, bool _isSocDien, StatManager _targetStats)
    {
        if (isDeah || isBong || isDongBang || isSocDien)
            return;

        if (_isBong)
        {
            isBong = true;
            bongTimer = 2;

            if (entityFX != null)
                entityFX.IgniteFxFor(2);
        }
        if (_isDongBang)
        {
            isDongBang = true;
            bangTimer = 2;

            Entity entity = GetComponent<Entity>();
            if (entity != null)
                entity.SlowEntity(.2f, 2);

            if (entityFX != null)
                entityFX.ChillFxFor(2);
        }

        if (_isSocDien)
        {
            isSocDien = true;
            socDienTimer = 2;

            if (entityFX != null)
                entityFX.ShockFxFor(2);
        }
    }

    private void TiaSet(StatManager _targetStats)
    {
        if (tiaSetPreFab == null || _targetStats == null)
            return;

        GameObject PreFab = Instantiate(tiaSetPreFab, transform.position, Quaternion.identity);
        TiaSetController ctrl = PreFab.GetComponent<TiaSetController>();
        if (ctrl != null)
            ctrl.SetUpTiaSet(Mathf.Max(1, satThuongDien.GetValue()), _targetStats);
    }

    private bool CanCrit()
    {
        int totalTileChiMang = tiLeChiMang.GetValue() + nhanhNhen.GetValue();

        if (Random.Range(0, 100) < totalTileChiMang)
            return true;

        return false;
    }
    private int CritDamege(int _damage)
    {
        float totalSatThuongChiMang = (satThuongChimang.GetValue() + sucManh.GetValue()) * .01f;

        float critDamage = _damage * totalSatThuongChiMang;

        return Mathf.RoundToInt(critDamage);
    }
    private int CheckTargerArmor(StatManager _targetStats, int totalDamage)
    {
        float effectiveArmor = _targetStats.giap.GetValue();

        if (_targetStats.isDongBang)
            effectiveArmor *= .8f;

        if (_targetStats.isXuyenGiap)
            effectiveArmor *= .5f;

        totalDamage -= Mathf.RoundToInt(effectiveArmor);
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool CheckTargetAvoidAttack(StatManager _targetStats)
    {
        int totalNe = _targetStats.ne.GetValue() +
                    _targetStats.nhanhNhen.GetValue();

        if (isSocDien)
            totalNe += 20;

        if (_targetStats.isSocDien)
            totalNe = Mathf.Max(0, totalNe - 20);

        if (Random.Range(0, 100) < totalNe)
        {
            return true;
        }
        return false;
    }

    public virtual void TakeDamage(int _damege)
    {
        if (isDeah)
            return;

        if (isXuyenGiap)
            _damege = Mathf.FloorToInt(_damege * 1.1f);

        DecreaseHealthBy(_damege);

        Entity entity = GetComponent<Entity>();
        if (entity != null && _damege > 0)
            entity.DamageEffect();

        if (currentHealth <= 0 && !isDeah)
        {
            isDeah = true;
            Deah();
        }
    }
    public virtual void BuffStats(int _modifier, float _duration, Stat _statModify)
    {
        if (_statModify == null || _modifier == 0)
            return;

        StartCoroutine(BuffDuraction(_modifier, _duration, _statModify));
    }

    private IEnumerator BuffDuraction(int _modifier, float _duration, Stat _statModify)
    {
        _statModify.AddMotdifier(_modifier);
        ClampHealthToMax();

        yield return new WaitForSeconds(_duration);

        _statModify.RemoveMotdifier(_modifier);
        ClampHealthToMax();
    }
    public void ClampHealthToMax()
    {
        int maxHp = GetMaxHealthValue();
        if (currentHealth > maxHp)
            currentHealth = maxHp;

        onHealthChanged?.Invoke();
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
