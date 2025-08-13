using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] AudioManager am;
    public Animator timeBreak;
    public Animator cycleBreak;
    public GameManager gameManager;
    public int wave;
    public TextMeshProUGUI waveText;

    [Header("Enemy Prefabs")]
    public GameObject meleeEnemyPrefab;
    public GameObject rangeEnemyPrefab;
    public GameObject bossEnemyPrefab;

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
    public int numOfBossEnemies;

    [Header("Add Enemy")]
    public int addMeleeEnemies;
    public int addRangeEnemies;
    public int addBossEnemies;

    public float hpMultiplier = 0;
    public float damageMultiplier = 0;
    public float coinMultiplier = 0;

    int sortOrder;

    private void Start()
    {
        am = FindAnyObjectByType<AudioManager>();
        wave = PlayerPrefs.GetInt("Checkpoint Wave", 1);
        CalculateEnemiesForWave(wave);
        StartWave();
        waveText.text = $"Wave: {wave}";
    }



    IEnumerator InitialSpawn()
    {
        float delay = totalSpawnTime / maxNumOfEnemiesInScene;
        for (int i = 0; i < maxNumOfEnemiesInScene; i++)
        {

            SpawnEnemy();

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
                SpawnEnemy();

                return;
            }

            if (activeEnemies.Count == 0 && enemyQueue.Count == 0)
            {
                canSpawnRemainingEnemies = false;
                endWave = true;

                if (wave % 10 == 0)
                {
                    timeBreak.Play("TimeBreak");
                    cycleBreak.Play("CycleBreak");
                    am.PlaySound(SoundType.TimeStop);
                }

                CalculateNextWave();
                StartWave();
            }
        }
    }

    public void SpawnEnemy()
    {
        int randomLoc = Random.Range(0, spawnLocations.Length);
        GameObject enemy = Instantiate(enemyQueue[0], spawnLocations[randomLoc].position, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.coinDrop = (int)(enemyScript.coinDrop * coinMultiplier);
        enemyScript.maxHealth = enemyScript.maxHealth * hpMultiplier;
        enemyScript.damage = enemyScript.damage * damageMultiplier;
        enemyScript.currentHealth = enemyScript.maxHealth;
        SpriteRenderer enemySprite = enemy.GetComponent<SpriteRenderer>();
        enemySprite.sortingOrder = sortOrder;
        sortOrder++;

        enemyQueue.RemoveAt(0);
        activeEnemies.Add(enemy);
    }

    public void StartWave()
    {
        if (wave < 11)
        {
            PlayerPrefs.SetInt("Checkpoint Wave", 1);
        }
        if (wave % 10 == 1)
        {
            
            gameManager.remainingTime = gameManager.initialTime;
            PlayerPrefs.SetInt("Checkpoint Wave", wave);
        }
        

        endWave = false;
        maxNumOfEnemiesInScene = 0;
        sortOrder = 0;

        for (int i = 0; i < numOfMeleeEnemies; i++)
        {
            enemyQueue.Add(meleeEnemyPrefab);
        }
        for (int i = 0; i < numOfRangeEnemies; i++)
        {
            enemyQueue.Add(rangeEnemyPrefab);
        }
        if (wave % 10 == 0)
        {
            for (int i = 0; i < numOfBossEnemies; i++)
            {
                enemyQueue.Add(bossEnemyPrefab);
            }
            
        }
        Shuffle(enemyQueue);

        maxNumOfEnemiesInScene = (int)(enemyQueue.Count * 0.15f);
        if (maxNumOfEnemiesInScene < 2)
        {
            maxNumOfEnemiesInScene = 2;
        }

        StartCoroutine(InitialSpawn());

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
        damageMultiplier = 1;
        coinMultiplier = 1;

        // Calculate for each wave from 1 to current wave
        for (int w = 1; w <= currentWave; w++)
        {

            if (w % 10 == 1)
            {
                if (w > 10)
                {
                    addMeleeEnemies++;
                    addRangeEnemies++;

                    hpMultiplier += hpMultiplier / 10;
                    hpMultiplier += hpMultiplier;

                    damageMultiplier += 0.5f;
                    coinMultiplier += 0.5f;
                }
                
            }

            if (w % 10 == 0)
            {
                //addBossEnemies++;
                numOfBossEnemies++;

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
            numOfBossEnemies = Mathf.Clamp(numOfBossEnemies, 0, 10);
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
