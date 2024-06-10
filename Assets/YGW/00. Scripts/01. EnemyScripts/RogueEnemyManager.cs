using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RogueEnemyManager : MonoBehaviour
{
    [SerializeField]
    private static RogueEnemyManager instance = null;

    [SerializeField]
    private int EnemyCount;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static RogueEnemyManager Instance
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

    public int GetEnemyCount()
    {
        return EnemyCount;
    }

    public void SetEnemyCount(int count)
    {
        EnemyCount = count;
    }

    public void DeclineEnemyCount()
    {
        EnemyCount--;
    }

    public void Initialize()
    {
        SetEnemyCount(GameObject.Find("EnemyManager").transform.childCount);
        InitEnemiesUI();
    }

    private void InitEnemiesUI()
    {
        GameObject parent = GameObject.Find("EnemyManager").gameObject;

        foreach (Transform child in parent.transform)
        {
            RogueEnemy rogueEnemy = child.GetComponentInChildren<RogueEnemy>();

            if (rogueEnemy != null)
            {
                rogueEnemy.InitEnemyUI();
                rogueEnemy.FindTargetPlayer();
            }
            else
            {
                Debug.LogWarning("RogueEnemy component not found on child: " + child.name);
            }
        }
    }
}
