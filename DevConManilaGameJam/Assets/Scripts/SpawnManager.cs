using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

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

        numOfMeleeEnemies += addMeleeEnemies;
        numOfRangeEnemies += addRangeEnemies;

        addMeleeEnemies++;
        addRangeEnemies++;
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
