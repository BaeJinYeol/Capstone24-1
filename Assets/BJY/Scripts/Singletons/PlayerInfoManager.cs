using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : Singleton<PlayerInfoManager>
{
    public int player_hp = 0;
    public int player_power = 0;
    public int player_speed = 0;
    public int player_attack_speed = 0;
    public int player_critical_rate = 0;
    public int player_attack_distance = 0;

    public Item helmet;
    public Item armor;
    public Item back;
    public Item weapon;
    public Item sub_weapon;

    public Item hair;
    public Item face_hair;
    public Item cloth;
    public Item feet;

    public int hair_id;
    public int face_hair_id;
    public int cloth_id;
    public int feet_id;

    public void PutOnItem(Item item)
    {
        switch (item.type)
        {
            case ItemType.Helmet:
                helmet = item;
                break;
            case ItemType.Armor:
                armor = item;
                break;
            case ItemType.Back:
                back = item;
                break;
            case ItemType.Weapon:
                weapon = item;
                break;
            case ItemType.SubWeapon:
                sub_weapon = item;
                break;
        }
        UpdateStat();
    }

    public void PutOnDress(Item item)
    {
        switch (item.type)
        {
            case ItemType.Hair:
                hair_id = item.item_id;
                hair = item;
                break;
            case ItemType.FaceHair:
                face_hair_id = item.item_id;
                face_hair = item;
                break;
            case ItemType.Cloth:
                cloth_id = item.item_id;
                cloth = item;
                break;
            case ItemType.Foot:
                feet_id = item.item_id;
                feet = item;
                break;
        }
    }

    public void TakeOffItem(Item item)
    {
        switch (item.type)
        {
            case ItemType.Helmet:
                helmet = null;
                break;
            case ItemType.Armor:
                armor = null;
                break;
            case ItemType.Back:
                back = null;
                break;
            case ItemType.Weapon:
                weapon = null;
                break;
            case ItemType.SubWeapon:
                sub_weapon = null;
                break;
        }
        UpdateStat();
    }   

    public void TakeOffDress(Item item)
    {
        switch (item.type)
        {
            case ItemType.Hair:
                hair_id = 0;
                hair = null;
                break;
            case ItemType.FaceHair:
                face_hair_id = 0;
                face_hair = null;
                break;
            case ItemType.Cloth:
                cloth_id = 0;
                cloth = null;
                break;
            case ItemType.Foot:
                feet_id = 0;
                feet = null;
                break;
        }
    }

    public void UpdateStat()
    {
        player_hp = 0;
        player_power = 0;
        player_speed = 0;
        player_attack_speed = 0;
        player_critical_rate = 0;
        player_attack_distance = 0;

        if (helmet != null)
        {
            player_hp += helmet.hp;
            player_power += helmet.power;
            player_speed += helmet.speed;
            player_attack_speed += helmet.attack_speed;
            player_critical_rate += helmet.critical_rate;
        }
        if (armor != null)
        {
            player_hp += armor.hp;
            player_power += armor.power;
            player_speed += armor.speed;
            player_attack_speed += armor.attack_speed;
            player_critical_rate += armor.critical_rate;
        }
        if (back != null)
        {
            player_hp += back.hp;
            player_power += back.power;
            player_speed += back.speed;
            player_attack_speed += back.attack_speed;
            player_critical_rate += back.critical_rate;
        }
        if (weapon != null)
        {
            player_hp += weapon.hp;
            player_power += weapon.power;
            player_speed += weapon.speed;
            player_attack_speed += weapon.attack_speed;
            player_critical_rate += weapon.critical_rate;
            player_attack_distance = weapon.distance;
        }
        if (sub_weapon != null)
        {
            player_hp += sub_weapon.hp;
            player_power += sub_weapon.power;
            player_speed += sub_weapon.speed;
            player_attack_speed += sub_weapon.attack_speed;
            player_critical_rate += sub_weapon.critical_rate;
        }

        GameObject.Find("MainSceneManager").GetComponent<MainSceneManager>().status.GetComponent<Status>().UpdateStat();
    }
}
