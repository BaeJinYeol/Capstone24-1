using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool is_save_data;

    public List<bool> clear_zone = new List<bool>();
    public List<int> own_items = new List<int>();

    public int player_gold;
    public int player_level;

    void Start()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        DataManager.Instance.JsonLoad();
    }

    public void NewGame()
    {
        player_gold = 1000; 
        player_level = 1;

        // clear_zone 배열 초기화
        for (int i = 0; i < 18; i++)
        {
            clear_zone[i] = false;
        }

        clear_zone[0] = true;

        own_items.Clear();
        
        // 기본 장비 제공: 갑옷, 무기
        own_items.Add(1);   // 갑옷
        own_items.Add(28);  // 투구
        own_items.Add(66);  // 무기
    }
}
