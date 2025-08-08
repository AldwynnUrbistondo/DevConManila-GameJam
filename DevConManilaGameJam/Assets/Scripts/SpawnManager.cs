using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int wave = 1;

    [Header("Enemy Prefabs")]
    public GameObject meleeEnemyPrefab;
    public GameObject rangeEnemyPrefab;

    public Transform[] spawnLocations;

    [SerializeField] public List<GameObject> enemyPrefabs = new List<GameObject>();

    public float totalSpawnTime = 30f;

    [Header("Enemy Count")]
    public int numOfMeleeEnemies;
    public int numOfRangeEnemies;

    [Header("Add Enemy")]
    public int addMeleeEnemies;
    public int addRangeEnemies;

    public float hpMultiplier = 0;

    private void Start()
    {
        CalculateEnemiesForWave(wave);
        StartWave();
    }

    IEnumerator SpawnEnemies()
    {
        float delay = totalSpawnTime / enemyPrefabs.Count;
        foreach (GameObject e in enemyPrefabs)
        {
            int randomLoc = Random.Range(0, spawnLocations.Length);
            GameObject enemy = Instantiate(e, spawnLocations[randomLoc].position, Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
        CalculateNextWave();
        yield return new WaitForSeconds(5);
        StartWave();
    }

    public void StartWave()
    {
        for (int i = 0; i < numOfMeleeEnemies; i++)
        {
            enemyPrefabs.Add(meleeEnemyPrefab);
        }
        for (int i = 0; i < numOfRangeEnemies; i++)
        {
            enemyPrefabs.Add(rangeEnemyPrefab);
        }
        Shuffle(enemyPrefabs);
        StartCoroutine(SpawnEnemies());
    }

    void CalculateNextWave()
    {
        enemyPrefabs.Clear();
        wave++;
        CalculateEnemiesForWave(wave);
    }

    public void CalculateEnemiesForWave(int currentWave)
    {
        // Reset counts to calculate from scratch
        numOfMeleeEnemies = 1;
        numOfRangeEnemies = 0;
        addMeleeEnemies = 1;
        addRangeEnemies = 1;
        hpMultiplier = 0;

        // Calculate for each wave from 1 to current wave
        for (int w = 1; w <= currentWave; w++)
        {
            if (w % 10 == 1 && w > 10)
            {
                addMeleeEnemies++;
                addRangeEnemies++;
            }

            if (w % 10 == 0)
            {
                hpMultiplier += 0.5f;
            }
            else
            {
                numOfMeleeEnemies += addMeleeEnemies;
                if (w >= 5)
                {
                    numOfRangeEnemies += addRangeEnemies;
                }
            }
            
            numOfMeleeEnemies = Mathf.Clamp(numOfMeleeEnemies, 0, 50);
            numOfRangeEnemies = Mathf.Clamp(numOfRangeEnemies, 0, 50);
        }

    }

 

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
