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
    public int item_id;            // ������ �ڵ�
    public string item_name;       // ������ �̸�
    public string description;     // ������ ����
    public string icon_path;       // ������ ������ ���
    public Sprite icon;            // ������ ������
    public int hp;                 // ü��
    public int power;              // ���ݷ�
    public int speed;              // �̵��ӵ�
    public int attack_speed;       // ���ݼӵ�
    public int critical_rate;      // ġ��Ÿ Ȯ��
    public ItemType type;          // ������ Ÿ��
    public int distance;
}
