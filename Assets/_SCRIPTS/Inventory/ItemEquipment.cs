
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Vukhi,
    Non,
    Giap,
    Quan
}


[CreateAssetMenu(fileName = "new ItemEquipment", menuName = "Data/Equipment")]
public class ItemEquipment: ItemData
{
    public EquipmentType equipmentType;

    public ItemEffect[] itemEffects;

    [Header("Major stats")]
    public int sucManh;
    public int nhanhNhen;
    public int thongMinh;
    public int sucSong; 

    [Header("OffDefensive stats")]
    public int damage;
    public int tiLeChiMang;
    public int satThuongChimang;

    [Header("Defensive stats")]
    public int MaxHealth;
    public int giap;
    public int ne;
    public int khangAP;

    [Header("Magic Stats")]
    public int satThuongLua;
    public int satThuongBang;
    public int satThuongAnhSang;

    [Header("Craft Material")]
    public List<InventoryItem> craftMaterials;
    public void AddModifiers()
    {
        var character = PlayerManager.instance.character.GetComponent<CharacterStat>();

        character.sucManh.AddMotdifier(sucManh);
        character.nhanhNhen.AddMotdifier(nhanhNhen);
        character.thongMinh.AddMotdifier(thongMinh);
        character.sucSong.AddMotdifier(sucSong);

        character.damage.AddMotdifier(damage);
        character.tiLeChiMang.AddMotdifier(tiLeChiMang);
        character.satThuongChimang.AddMotdifier(satThuongChimang);

        character.MaxHealth.AddMotdifier(MaxHealth);
        character.giap.AddMotdifier(giap);
        character.ne.AddMotdifier(ne);
        character.khangAP.AddMotdifier(khangAP);

        character.satThuongLua.AddMotdifier(satThuongLua);
        character.satThuongBang.AddMotdifier(satThuongBang);
        character.satThuongDien.AddMotdifier(satThuongAnhSang);

    }
    public void RemoveModifiers()
    {
        var character = PlayerManager.instance.character.GetComponent<CharacterStat>();

        character.sucManh.RemoveMotdifier(sucManh);
        character.nhanhNhen.RemoveMotdifier(nhanhNhen);
        character.thongMinh.RemoveMotdifier(thongMinh);
        character.sucSong.RemoveMotdifier(sucSong);

        character.damage.RemoveMotdifier(damage);
        character.tiLeChiMang.RemoveMotdifier(tiLeChiMang);
        character.satThuongChimang.RemoveMotdifier(satThuongChimang);

        character.MaxHealth.RemoveMotdifier(MaxHealth);
        character.giap.RemoveMotdifier(giap);
        character.ne.RemoveMotdifier(ne);
        character.khangAP.RemoveMotdifier(khangAP);

        character.satThuongLua.RemoveMotdifier(satThuongLua);
        character.satThuongBang.RemoveMotdifier(satThuongBang);
        character.satThuongDien.RemoveMotdifier(satThuongAnhSang);
    }
    
    public void ItemEffect(Transform _enemyPos)
    {
        foreach(var item in itemEffects)
        {
            item.ExecuteEffect(_enemyPos);
        }
    }
}
