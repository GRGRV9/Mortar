using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Text enemiesCounter;

    [SerializeField]
    GameObject enemyPrefab1;

    [SerializeField]
    Transform player;

    int currentEnemiesCount;

    private void Awake()
    {
        currentEnemiesCount = transform.childCount;
    }

    public void SpawnWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab1, transform.position + new Vector3(Random.Range(-10,10),0, Random.Range(-10, 10)), transform.rotation);
            enemy.transform.parent = gameObject.transform;
            enemy.GetComponent<EnemyMovement>().SetTargetTransform(player);
            RefreshEnemiesCount();    
        }        
    }

    public void DeathCallback()
    {
        RefreshEnemiesCount();
    }
    
    public void RefreshEnemiesCount()
    {
        StartCoroutine("RefreshWithDelay");
    }

    IEnumerator RefreshWithDelay()
    {
        currentEnemiesCount = transform.childCount;
        enemiesCounter.text = currentEnemiesCount.ToString();
        yield return new WaitForSeconds(1.6f);
        currentEnemiesCount = transform.childCount;
        enemiesCounter.text = currentEnemiesCount.ToString();
        Debug.Log(currentEnemiesCount);
    }
    public int GetEnemiesCount()
    {
        return currentEnemiesCount;
    }

}
