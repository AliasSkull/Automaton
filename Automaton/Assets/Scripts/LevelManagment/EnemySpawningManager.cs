using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NodeCanvas;

public class EnemySpawningManager : MonoBehaviour
{
    public GameObject meleeEnemyPool;
    public GameObject rangedEnemyPool;

    public GameObject enemyPrefab;

    [System.Serializable]
    public class EnemySpawnerLevel
    {
        public string name;

        [System.Serializable]
        public class Wave
        {
            public string name;
            
            public enum enemyType
            {
                melee,
                ranged,
                specialMelee,
                specialRanged
            }
            
            public List<enemyType> enemiesInWave;
            public GameObject enemySpawnPoints;
            [Header("For Current Wave")]
            public int enemyThreshhold;
            public float SpawnTiming;
            [Header ("Max Time Till Wave Switches")]
            public float timeTillNextWave;
        }

        public Wave[] waves = new Wave[3];
    }

    public EnemySpawnerLevel[] levels = new EnemySpawnerLevel[4];

    public int currentLevel = 0;
    public int currentWave = 0;
    private int enemiesAlive;
    private int enemiesSpawnedThisWave;

    private float timer;
    private float timeBetweenSpawns;

    private bool waveStarted;
    private bool levelStarted;
    private bool levelSpawningDone;

    private int enemiesAlive1;
    private int enemiesAlive2;
    private int enemiesAlive3;

    private int enemiesSpawnedInWave1;
    private int enemiesSpawnedInWave2;
    private int enemiesSpawnedInWave3;

    // Start is called before the first frame update
    void Start()
    {   
        //start it all through wave spawning
        //WaveSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            WaveSpawning();
        }
        
        if (waveStarted && levelStarted)
        {
            timer += Time.deltaTime;
        }

        if (currentLevel < levels.Length && levelStarted)
        {
            ChangeCheck();
        }
    }

    public void EnemyDeathReset(GameObject goblin, int wave)
    {
        if(wave == 0)
        {
            enemiesAlive1--;
        }
        else if(wave == 1)
        {
            enemiesAlive2--;

        }
        else if (wave == 2)
        {
            enemiesAlive3--;
        }

        enemiesAlive--;

        Destroy(goblin);
    }

    public void ChangeCheck()
    {
        if (timer >= levels[currentLevel].waves[currentWave].timeTillNextWave && waveStarted)
        {
            WaveChange();
        }

        if(currentWave == 0 && enemiesAlive1 == 0 && levelStarted)
        {
            WaveChange();
        }
        else if (currentWave == 1 && enemiesAlive2 == 0 && levelStarted)
        {
            WaveChange();
        }


        if (!levelSpawningDone && enemiesSpawnedInWave3 == levels[currentLevel].waves[2].enemiesInWave.Count)
        {
            levelSpawningDone = true;
        }

        if (levelSpawningDone && enemiesAlive == 0)
        {
            LevelChange();
        }
    }

    public void WaveChange() //starts through wave change check
    {
        waveStarted = false;
        if(currentWave + 1 < levels[currentLevel].waves.Length)
        {
            levelStarted = true;
            timer = 0;

            currentWave++;
            enemiesSpawnedThisWave = 0;

            timeBetweenSpawns = levels[currentLevel].waves[currentWave].SpawnTiming / levels[currentLevel].waves[currentWave].enemiesInWave.Count;


            WaveSpawning();

            waveStarted = true;
        }
        else
        {
            levelSpawningDone = true;
        }
    }

    public void LevelChange() //starts when all enemies from that level are dead
    {
        levelStarted = false;
        waveStarted = false;
        levelSpawningDone = false;
        timer = 0;
        enemiesSpawnedThisWave = 0;
        enemiesSpawnedInWave3 = 0;
        currentLevel++;

        GameObject.Find("LevelManager").gameObject.GetComponent<LevelManager>().OpenDoor();

        print("levelhange");
    }

    public void WaveSpawning() //Starts at the beginning of a wave through wave change or level change
    {
        timeBetweenSpawns = levels[currentLevel].waves[currentWave].SpawnTiming / levels[currentLevel].waves[currentWave].enemiesInWave.Count;

        if (enemiesSpawnedThisWave < levels[currentLevel].waves[currentWave].enemiesInWave.Count)
        {
            if(levels[currentLevel].waves[currentWave].enemiesInWave[enemiesSpawnedThisWave] == EnemySpawnerLevel.Wave.enemyType.melee)
            {
                GameObject enemy = meleeEnemyPool.transform.GetChild(0).gameObject;
                StartCoroutine(SpawnEnemy(timeBetweenSpawns));
            }
            else if (levels[currentLevel].waves[currentWave].enemiesInWave[enemiesSpawnedThisWave] == EnemySpawnerLevel.Wave.enemyType.ranged)
            {
                GameObject enemy = rangedEnemyPool.transform.GetChild(0).gameObject;
                StartCoroutine(SpawnEnemy(timeBetweenSpawns));
            }
            waveStarted = true;
        }
    }

    public IEnumerator SpawnEnemy(float time)
    {
        yield return new WaitForSeconds(time);

        int spawnPoint = Random.Range(0, levels[currentLevel].waves[currentWave].enemySpawnPoints.transform.childCount - 1);

        GameObject enemy = Instantiate(enemyPrefab, levels[currentLevel].waves[currentWave].enemySpawnPoints.transform.GetChild(spawnPoint).position, enemyPrefab.transform.rotation);

        enemiesSpawnedThisWave++;
        enemiesAlive++;

        if(currentWave == 0)
        {
            enemiesSpawnedInWave1++;
            enemiesAlive1++;

            enemy.GetComponent<Goblin>().wave = 0;
        }
        else if (currentWave == 1)
        {
            enemiesSpawnedInWave2++;
            enemiesAlive2++;

            enemy.GetComponent<Goblin>().wave = 1;
        }
        else if (currentWave == 2)
        {
            enemiesSpawnedInWave3++;
            enemiesAlive3++;

            enemy.GetComponent<Goblin>().wave = 2;
        }

        levelStarted = true;

        WaveSpawning();

    }

    public IEnumerator ActivateGobSprite(SpriteRenderer sprite)
    {

        yield return new WaitForSeconds(0.2f);
        sprite.enabled = true;
    }

}
