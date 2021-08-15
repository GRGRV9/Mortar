using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    EnemySpawner enemySpawner;

    [SerializeField]
    Text textUI;

    [SerializeField]
    Text waveCounter;

    UpgradeTargetsSpawner targetsSpawner;

    int _currentWave = 0;
    int enemiesCount;
    bool checkingEnemies;
        
    void Start()
    {
        targetsSpawner = GetComponentInChildren<UpgradeTargetsSpawner>();

        StartCoroutine("StartWave");
    }
    
    void Update()
    {
        enemiesCount = enemySpawner.GetEnemiesCount();

        if (enemiesCount == 0 && checkingEnemies)
        {
            StartCoroutine("StartWave");
        }
    }

    private void SpawnWave(int currentWave)
    {
        switch (currentWave)
        {
            case 1:
                enemySpawner.SpawnWave(4);
                break;
            case 2:
                enemySpawner.SpawnWave(6);
                break;
            case 3:
                enemySpawner.SpawnWave(8);
                break;
            case 4:
                enemySpawner.SpawnWave(10);
                break;
            case 5:
                enemySpawner.SpawnWave(12);
                break;
            case 6:
                enemySpawner.SpawnWave(14);
                break;
            case 7:
                enemySpawner.SpawnWave(16);
                break;
            case 8:
                enemySpawner.SpawnWave(18);
                break;
            case 9:
                enemySpawner.SpawnWave(20);
                break;
            case 10:
                enemySpawner.SpawnWave(22);
                break;
        }
    }

    public int GetCurrentWave()
    {
        return _currentWave;
    }

    IEnumerator StartWave()
    {
        checkingEnemies = false;
        targetsSpawner.InstantiateUpgradeTargets();
        textUI.text = "<elastic>3</elastic>";
        yield return new WaitForSeconds(1);
        textUI.text = "<elastic>2</elastic>";
        yield return new WaitForSeconds(1);
        textUI.text = "<elastic>1</elastic>";
        yield return new WaitForSeconds(1);
        textUI.text = "<elastic2>START</elastic2>";
        _currentWave++;
        SpawnWave(_currentWave);
        waveCounter.text = _currentWave.ToString();
        checkingEnemies = true;
        yield return new WaitForSeconds(2);
        textUI.text = " ";        
    }    

}
