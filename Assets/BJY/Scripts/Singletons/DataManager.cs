using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public List<bool> ClearZone = new List<bool>();
    public List<int> OwnItems = new List<int>();
    public int[] PutOnItem = new int[5];
    public int[] PutOnDress = new int[4];

    public int gold;
    public int level;
}

public class DataManager : Singleton<DataManager>
{
    string path;

    void Start()
    {
        path = Application.persistentDataPath + "/" + "userdata.json";
        if (!File.Exists(path))
        {
            GameManager.Instance.is_save_data = false;
        }
        else
        {
            GameManager.Instance.is_save_data = true;
        }
    }

    public void JsonLoad()
    {
        SaveData save_data = new SaveData();

        if (!File.Exists(path))
        {
            Debug.Log("No data");
            return;
        }
        else
        {
            string load_json = File.ReadAllText(path);
            save_data = JsonUtility.FromJson<SaveData>(load_json);

            if(save_data != null)
            {
                GameManager.Instance.clear_zone = save_data.ClearZone;
                GameManager.Instance.own_items = save_data.OwnItems;
                GameManager.Instance.player_gold = save_data.gold;
                GameManager.Instance.player_level = save_data.level;

                if (save_data.PutOnItem[0] != 0)
                    PlayerInfoManager.Instance.helmet = ItemDatabase.Instance.items[save_data.PutOnItem[0] - 1];
                else
                    PlayerInfoManager.Instance.helmet = null;

                if (save_data.PutOnItem[1] != 0)
                    PlayerInfoManager.Instance.armor = ItemDatabase.Instance.items[save_data.PutOnItem[1] - 1];
                else
                    PlayerInfoManager.Instance.armor = null;

                if (save_data.PutOnItem[2] != 0)
                    PlayerInfoManager.Instance.back = ItemDatabase.Instance.items[save_data.PutOnItem[2] - 1];
                else
                    PlayerInfoManager.Instance.back = null; 

                if (save_data.PutOnItem[3] != 0)
                    PlayerInfoManager.Instance.weapon = ItemDatabase.Instance.items[save_data.PutOnItem[3] - 1];
                else
                    PlayerInfoManager.Instance.weapon = null; 

                if (save_data.PutOnItem[4] != 0)
                    PlayerInfoManager.Instance.sub_weapon = ItemDatabase.Instance.items[save_data.PutOnItem[4] - 1];
                else
                    PlayerInfoManager.Instance.sub_weapon = null;

                PlayerInfoManager.Instance.hair_id = save_data.PutOnDress[0];
                PlayerInfoManager.Instance.face_hair_id = save_data.PutOnDress[1];
                PlayerInfoManager.Instance.cloth_id = save_data.PutOnDress[2];
                PlayerInfoManager.Instance.feet_id = save_data.PutOnDress[3];
            }
        }
    }

    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        saveData.ClearZone = GameManager.Instance.clear_zone;
        saveData.OwnItems = GameManager.Instance.own_items;
        saveData.gold = GameManager.Instance.player_gold;
        saveData.level = GameManager.Instance.player_level;

        if (PlayerInfoManager.Instance.helmet != null && PlayerInfoManager.Instance.helmet.item_id > 0)
            saveData.PutOnItem[0] = PlayerInfoManager.Instance.helmet.item_id;
        if (PlayerInfoManager.Instance.armor != null && PlayerInfoManager.Instance.armor.item_id > 0)
            saveData.PutOnItem[1] = PlayerInfoManager.Instance.armor.item_id;
        if (PlayerInfoManager.Instance.back != null && PlayerInfoManager.Instance.back.item_id > 0)
            saveData.PutOnItem[2] = PlayerInfoManager.Instance.back.item_id;
        if (PlayerInfoManager.Instance.weapon != null && PlayerInfoManager.Instance.weapon.item_id > 0)
            saveData.PutOnItem[3] = PlayerInfoManager.Instance.weapon.item_id;
        if (PlayerInfoManager.Instance.sub_weapon != null && PlayerInfoManager.Instance.sub_weapon.item_id > 0)
            saveData.PutOnItem[4] = PlayerInfoManager.Instance.sub_weapon.item_id;

        saveData.PutOnDress[0] = PlayerInfoManager.Instance.hair_id;
        saveData.PutOnDress[1] = PlayerInfoManager.Instance.face_hair_id;
        saveData.PutOnDress[2] = PlayerInfoManager.Instance.cloth_id;
        saveData.PutOnDress[3] = PlayerInfoManager.Instance.feet_id;

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }
}
