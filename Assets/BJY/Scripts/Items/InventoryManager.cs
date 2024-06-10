using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private List<int> inven_id = new List<int>();
    private List<Item> inven_item = new List<Item>();

    public GameObject item_armor_;
    public GameObject item_;
    public Transform inven_transform;

    public Transform inven_helmet;
    public Transform inven_armor;
    public Transform inven_back;
    public Transform inven_weapon;
    public Transform inven_subweapon;

    public Transform item_content;
    public Transform dress_content;

    public Transform item_box;
    public Transform dress_box;

    public Transform player_root;

    // Start is called before the first frame update
    void Start()
    {
        inven_id = GameManager.Instance.own_items;
        LoadItem();
        for (int i = 0; i < inven_item.Count; i++)
        {
            GenerateItem(inven_item[i]);
        }
        LoadPutOnItem();
        LoadPutOnDress();
    }

    public void LoadPutOnItem()
    {
        if (PlayerInfoManager.Instance.helmet.item_id != 0)
        {
            Item item = PlayerInfoManager.Instance.helmet;
            GameObject helmet = item_content.Find($"Item_{item.icon.name}").gameObject;
            helmet.GetComponent<ItemEvent>().OnMouseDoubleClick();
        }
        if (PlayerInfoManager.Instance.armor.item_id != 0)
        {
            Item item = PlayerInfoManager.Instance.armor;
            GameObject armor = item_content.Find($"Item_Armor_{item.item_id}").gameObject;
            armor.GetComponent<ItemEvent>().OnMouseDoubleClick();
        }
        if (PlayerInfoManager.Instance.back.item_id != 0)
        {
            Item item = PlayerInfoManager.Instance.back;
            GameObject back = item_content.Find($"Item_{item.icon.name}").gameObject;
            back.GetComponent<ItemEvent>().OnMouseDoubleClick();
        }
        if (PlayerInfoManager.Instance.weapon.item_id != 0)
        {
            Item item = PlayerInfoManager.Instance.weapon;
            GameObject weapon = item_content.Find($"Item_{item.icon.name}").gameObject;
            weapon.GetComponent<ItemEvent>().OnMouseDoubleClick();
        }
        if (PlayerInfoManager.Instance.sub_weapon.item_id != 0)
        {
            Item item = PlayerInfoManager.Instance.sub_weapon;
            GameObject sub_weapon = item_content.Find($"Item_{item.icon.name}").gameObject;
            sub_weapon.GetComponent<ItemEvent>().OnMouseDoubleClick();
        }
    }

    public void LoadPutOnDress()
    {
        if (PlayerInfoManager.Instance.hair_id != 0)
        {
            int id = PlayerInfoManager.Instance.hair_id;
            GameObject hair = dress_content.GetChild(id - 1).gameObject;
            hair.GetComponent<ItemEvent>().OnMouseDoubleClick();
        }
        if (PlayerInfoManager.Instance.face_hair_id != 0)
        {
            int id = PlayerInfoManager.Instance.face_hair_id;
            GameObject facehair = dress_content.GetChild(id - 1 + 42).gameObject;
            facehair.GetComponent<ItemEvent>().OnMouseDoubleClick();
        }
        if (PlayerInfoManager.Instance.cloth_id != 0)
        {
            if (PlayerInfoManager.Instance.cloth_id == 100)
            {
                GameObject cloth = dress_content.GetChild(90).gameObject;
                cloth.GetComponent<ItemEvent>().OnMouseDoubleClick();
            }
            else
            {
                int id = PlayerInfoManager.Instance.cloth_id;
                GameObject cloth = dress_content.GetChild(id - 1 + 42 + 7).gameObject;
                cloth.GetComponent<ItemEvent>().OnMouseDoubleClick();
            }
        }
        if (PlayerInfoManager.Instance.feet_id != 0)
        {
            if (PlayerInfoManager.Instance.cloth_id == 100)
            {
                GameObject cloth = dress_content.GetChild(91).gameObject;
                cloth.GetComponent<ItemEvent>().OnMouseDoubleClick();
            }
            else
            {
                int id = PlayerInfoManager.Instance.feet_id;
                GameObject feet = dress_content.GetChild(id - 1 + 42 + 7 + 25).gameObject;
                feet.GetComponent<ItemEvent>().OnMouseDoubleClick();
            }
        }
    }

    public void LoadItem()
    {
        for (int i = 0; i < inven_id.Count; i++)
        {
            inven_item.Add(ItemDatabase.Instance.items[inven_id[i] - 1]);
        }
    }

    public void GenerateItem(Item gen_item)
    {
        if (gen_item.type == ItemType.Armor) 
        {
            GameObject new_item = Instantiate(item_armor_, inven_transform);
            new_item.GetComponent<ItemEvent>().item = gen_item;
            new_item.name = item_armor_.name + $"{gen_item.item_id}";
            Sprite[] icons = Resources.LoadAll<Sprite>(gen_item.icon_path);
            for (int i = 0; i < icons.Length; i++)
            {
                Image icon = new_item.transform.GetChild(i).GetComponent<Image>();
                icon.sprite = icons[i];
                icon.SetNativeSize();
            }
        }
        else
        {
            GameObject new_item = Instantiate(item_, inven_transform);
            new_item.GetComponent<ItemEvent>().item = gen_item;
            new_item.name = item_.name + $"{gen_item.icon.name}";
            Image icon = new_item.transform.GetChild(0).GetComponent<Image>();
            icon.sprite = Resources.Load<Sprite>(gen_item.icon_path);
            icon.SetNativeSize();
        }
    }
}
