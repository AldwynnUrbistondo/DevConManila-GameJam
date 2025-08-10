using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public int wave = 1;
    public TextMeshProUGUI waveText;

    [Header("Enemy Prefabs")]
    public GameObject meleeEnemyPrefab;
    public GameObject rangeEnemyPrefab;

    public Transform[] spawnLocations;

    [Header("Wave Variables")]
    [SerializeField] public List<GameObject> enemyQueue = new List<GameObject>();
    [SerializeField] public List<GameObject> activeEnemies = new List<GameObject>();
    public bool canSpawnRemainingEnemies = false;
    public bool endWave = false;
    public int maxNumOfEnemiesInScene;
    public float totalSpawnTime = 5f;

    [Header("Enemy Count")]
    public int numOfMeleeEnemies;
    public int numOfRangeEnemies;

    [Header("Add Enemy")]
    public int addMeleeEnemies;
    public int addRangeEnemies;

    public float hpMultiplier = 0;
    public float coinMultiplier = 0;

    private void Start()
    {
        CalculateEnemiesForWave(wave);
        StartWave();
        waveText.text = $"Wave: {wave}";
    }

    IEnumerator SpawnEnemies()
    {
        float delay = totalSpawnTime / maxNumOfEnemiesInScene;
        for (int i = 0; i < maxNumOfEnemiesInScene; i++)
        {
            int randomLoc = Random.Range(0, spawnLocations.Length);
            GameObject enemy = Instantiate(enemyQueue[0], spawnLocations[randomLoc].position, Quaternion.identity);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.coinDrop = (int)(enemyScript.coinDrop * coinMultiplier);
            enemyScript.maxHealth = enemyScript.maxHealth * hpMultiplier;
            enemyScript.currentHealth = enemyScript.maxHealth;


            enemyQueue.RemoveAt(0);
            activeEnemies.Add(enemy);

            yield return new WaitForSeconds(delay);
        }
        canSpawnRemainingEnemies = true;

        //CalculateNextWave();
        //yield return new WaitForSeconds(5);
        //StartWave();
    }

    private void Update()
    {
        // Check Destroyed or Dead Enemies
        activeEnemies.RemoveAll(enemy => enemy == null);

        if (canSpawnRemainingEnemies && !endWave)
        {
            if (activeEnemies.Count != maxNumOfEnemiesInScene && enemyQueue.Count > 0)
            {
                int randomLoc = Random.Range(0, spawnLocations.Length);
                GameObject enemy = Instantiate(enemyQueue[0], spawnLocations[randomLoc].position, Quaternion.identity);
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.coinDrop = (int)(enemyScript.coinDrop * coinMultiplier);
                enemyScript.maxHealth = enemyScript.maxHealth * hpMultiplier;
                enemyScript.currentHealth = enemyScript.maxHealth;


                enemyQueue.RemoveAt(0);
                activeEnemies.Add(enemy);

                return;
            }

            if (activeEnemies.Count == 0 && enemyQueue.Count == 0)
            {
                canSpawnRemainingEnemies = false;
                endWave = true;

                CalculateNextWave();
                StartWave();
            }
        }
    }

    public void StartWave()
    {
        endWave = false;
        maxNumOfEnemiesInScene = 0;

        for (int i = 0; i < numOfMeleeEnemies; i++)
        {
            enemyQueue.Add(meleeEnemyPrefab);
        }
        for (int i = 0; i < numOfRangeEnemies; i++)
        {
            enemyQueue.Add(rangeEnemyPrefab);
        }
        Shuffle(enemyQueue);

        maxNumOfEnemiesInScene = (int)(enemyQueue.Count * 0.15f);
        if (maxNumOfEnemiesInScene < 2)
        {
            maxNumOfEnemiesInScene = 2;
        }

        StartCoroutine(SpawnEnemies());
    }

    void CalculateNextWave()
    {
        enemyQueue.Clear();
        wave++;
        waveText.text = $"Wave: {wave}";
        CalculateEnemiesForWave(wave);
    }

    public void CalculateEnemiesForWave(int currentWave)
    {
        // Reset counts to calculate from scratch
        numOfMeleeEnemies = 1;
        numOfRangeEnemies = 0;
        addMeleeEnemies = 1;
        addRangeEnemies = 1;
        hpMultiplier = 1;
        coinMultiplier = 1;

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
                coinMultiplier += 0.5f;
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
