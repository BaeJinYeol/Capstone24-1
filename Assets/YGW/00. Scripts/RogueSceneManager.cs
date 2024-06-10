using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RogueSceneManager : MonoBehaviour
{
    [SerializeField]
    private static RogueSceneManager instance = null;

    [SerializeField]
    private TMP_Text item_text;
    [SerializeField]
    private TMP_Text result_text;
    [SerializeField]
    private GameObject result_canvas;

    public bool isOver = false;
    public AudioClip button_click_clip;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static RogueSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded : " + scene.name);
        LoadEnemyInfo();
        SetupPlayerPosition();
        SetupEnemyBehavior();
    }

    public void LoadEnemyInfo()
    {
        RogueEnemyManager.Instance.Initialize();
    }

    public void SetupPlayerPosition()
    {
        RoguePlayer player = GameObject.Find("RoguePlayer").GetComponentInChildren<RoguePlayer>();
        player.transform.position = new Vector2(0, 0);
    }

    public void SetupEnemyBehavior()
    {
        GameObject enemyManager = GameObject.Find("EnemyManager");
        // RogueEnemyBehavior[] enemies = enemyManager.GetComponentsInChildren<RogueEnemyBehavior>();
        RogueEnemyAi[] enemies = enemyManager.GetComponentsInChildren<RogueEnemyAi>();

        foreach (var child in enemies)
        {
            child.InitComponent();
        }

    }

    public void ClearRogue(int stage)
    {
        int new_itemId = 0;
        if (stage == 1)
        {
            new_itemId = RandomItem(1, 50);
            GameManager.Instance.player_gold += 500;
        }
        else if (stage == 2)
        {
            new_itemId = RandomItem(50, 86);
            GameManager.Instance.player_gold += 700;
        }

        isOver = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GameManager.Instance.own_items.Add(new_itemId);
        item_text.text = $"[{ItemDatabase.Instance.items[new_itemId - 1].item_name}] NEW!";
        Time.timeScale = 0f;
        result_text.text = "CLEAR";
        result_canvas.SetActive(true);
    }

    public void FailRogue()
    {
        isOver = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0f;
        result_text.text = "FAIL";
        result_canvas.SetActive(true);
    }

    private int RandomItem(int min, int max)
    {
        // min 이상 max 미만
        return UnityEngine.Random.Range(min, max);
    }

    public void ClickHomeButton()
    {
        Debug.Log("ClickHome");
        SceneManager.LoadScene("MainScene");
        DestroyRogueObject();
    }

    private void DestroyRogueObject()
    {
        Destroy(GameObject.Find("RoguePlayer"));
        Destroy(GameObject.Find("RogueCanvas"));
        Destroy(RogueEnemyManager.Instance.gameObject);
        Destroy(this.gameObject);
    }
    public void PlaySfxSound()
    {
        AudioManager.Instance.PlaySFXSound("BtnSound", button_click_clip);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
