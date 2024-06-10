using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private EnemySpawner enemySpawner;
    private int currentWaveIndex = -1;

    [SerializeField]
    private PlayerHP playerHP;

    private bool isClick = false;
    private bool isStop = false;

    public int CurrentWave => currentWaveIndex + 1;
    public int MaxWave => waves.Length;

    public void StartButton()
    {
        if (!isClick)
        {
            StartWave();
            isClick = true;
        }
        else
        {
            if (isStop)
            {
                Time.timeScale = 1;
                isStop = false;
            }
            else
            {
                Time.timeScale = 0;
                isStop = true;
            }
        }

    }

    public void StartWave()
    {
        if (/*enemySpawner.EnemyList.Count == 0 &&*/ currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;
            enemySpawner.StartWave(waves[currentWaveIndex]);
        }
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
    public float nextWaveDelay;
}
