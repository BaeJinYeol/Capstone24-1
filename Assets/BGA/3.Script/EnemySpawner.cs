using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private PlayerHP playerHP;
    [SerializeField]
    private PlayerGold playerGold;
    [SerializeField]
    private WaveSystem waveSystem;

    private Wave currentWave;

    private List<M_Enemy> enemyList;
    public List<M_Enemy> EnemyList => enemyList;

    private bool isEnd = true;
    private bool isClick = false;

    private void Awake()
    {
        enemyList = new List<M_Enemy> ();
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave;
        StartCoroutine("SpawnEnemy");
    }

    private void FixedUpdate()
    {
        if(waveSystem.CurrentWave == waveSystem.MaxWave && enemyList.Count == 0 && isEnd )
        {
            if (playerHP.CurrentHP > 0)
            {
                StopCoroutine("SpawnEnemy");
                playerHP.GameEnd(false);
                return;
            }
            else
            {
                StopCoroutine("SpawnEnemy");
                playerHP.GameEnd(true);
                return;
            }
        }
    }

    private IEnumerator SpawnEnemy()
    {
        int spawnEnemyCount = 0;
        isEnd = false;

        while(spawnEnemyCount < currentWave.maxEnemyCount)
        {
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            M_Enemy enemy = clone.GetComponent<M_Enemy>();

            enemy.Setup(this, wayPoints);
            EnemyList.Add(enemy);

            SpawnEnemyHPSlider(clone);

            spawnEnemyCount++;

            yield return new WaitForSeconds(currentWave.spawnTime);
        }
        yield return new WaitForSeconds(currentWave.nextWaveDelay);
        isEnd = true;
        waveSystem.StartWave();
    }

    public void DestroyEnemy(EnemyDestroyType type, M_Enemy enemy, int gold)
    {
        if(type == EnemyDestroyType.Arrive)
        {
            playerHP.TakeDamage(1);
        }
        else if(type == EnemyDestroyType.Kill)
        {
            playerGold.CurrentGold += gold;
        }
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<SliderPositionSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<M_EnemyHP_View>().Setup(enemy.GetComponent<M_Enemy_HP>());
    }
}
