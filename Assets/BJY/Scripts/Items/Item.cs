using UnityEngine;

public enum ItemType
{
    Armor,                  
    Back,                        
    Cloth,                       
    Hair,                       
    FaceHair,                    
    Foot,                        
    Helmet,                        
    Weapon,
    SubWeapon
}

[System.Serializable]
public class Item
{
    public int item_id;            // 아이템 코드
    public string item_name;       // 아이템 이름
    public string description;     // 아이템 설명
    public string icon_path;       // 아이템 아이콘 경로
    public Sprite icon;            // 아이템 아이콘
    public int hp;                 // 체력
    public int power;              // 공격력
    public int speed;              // 이동속도
    public int attack_speed;       // 공격속도
    public int critical_rate;      // 치명타 확률
    public ItemType type;          // 아이템 타입
    public int distance;
}
