using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate[] towerTemplate;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private PlayerGold playerGold;

    private bool isOnTowerButton = false;
    private GameObject followTowerClone = null;

    private bool trigger = false;
    private int towerType;

    public void ReadyToSpawnTower(int type)
    {
        towerType = type;
        if(isOnTowerButton == true)
        {
            return;
        }
        if (towerTemplate[towerType].weapon[0].cost > playerGold.CurrentGold)
        {
            //systemTextView.PrintText(SystemType.Money);
            return;
        }
        isOnTowerButton = true;
        followTowerClone = Instantiate(towerTemplate[towerType].followtowerPrefab);
        StartCoroutine("OnTowerCancelSystem");
    }

    public void SpawnTower(Transform tileTransform)
    {
        if (isOnTowerButton == false)
        {
            return;
        }

        Tile tile = tileTransform.GetComponent<Tile>();
        if (tile.IsBuildTower == true)
        {
            return;
        }

        isOnTowerButton = false;
        tile.IsBuildTower = true;
        playerGold.CurrentGold -= towerTemplate[towerType].weapon[0].cost;
        GameObject clone = Instantiate(towerTemplate[towerType].towerPrefab, tileTransform.position, Quaternion.identity);
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
        Destroy(followTowerClone);
        StopCoroutine("OnTowerCancelSystem");
    }

    private IEnumerator OnTowerCancelSystem()
    {
        while (true)
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnTowerButton = false;
                Destroy(followTowerClone);
                break;
            }
            yield return null;
        }
        
    }
}
