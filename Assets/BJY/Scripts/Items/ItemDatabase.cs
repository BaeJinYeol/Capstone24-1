using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : Singleton<ItemDatabase>
{
    private const string itemDB_path = "data/itemDB";

    public List<Item> items = new List<Item>();

    private void Start()
    {
        TextAsset jsonTextFile = Resources.Load<TextAsset>(itemDB_path);

        var loaded_items = JsonHelper.FromJson<Item>(jsonTextFile.text);
        Debug.Log(loaded_items.Length);

        foreach (Item item in loaded_items)
        {
            item.icon = Resources.Load<Sprite>(item.icon_path);
            items.Add(item);
        }

    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.data;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.data = array;
            return JsonUtility.ToJson(wrapper);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] data;
        }
    }
}
