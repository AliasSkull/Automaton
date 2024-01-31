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

    public void EnemyDeathReset(GameObject goblin)
    {
        enemiesAlive--;

        Destroy(goblin);
    }

    public void ChangeCheck()
    {
        if (timer >= levels[currentLevel].waves[currentWave].timeTillNextWave && waveStarted)
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
                StartCoroutine(SpawnEnemy(timeBetweenSpawns, enemy));
            }
            else if (levels[currentLevel].waves[currentWave].enemiesInWave[enemiesSpawnedThisWave] == EnemySpawnerLevel.Wave.enemyType.ranged)
            {
                GameObject enemy = rangedEnemyPool.transform.GetChild(0).gameObject;
                StartCoroutine(SpawnEnemy(timeBetweenSpawns, enemy));
            }
            waveStarted = true;
        }
    }

    public IEnumerator SpawnEnemy(float time, GameObject enemy)
    {
        yield return new WaitForSeconds(time);
        levelStarted = true;

        Instantiate(enemyPrefab, levels[currentLevel].waves[currentWave].enemySpawnPoints.transform.GetChild(0).position, enemyPrefab.transform.rotation);

        enemiesSpawnedThisWave++;
        enemiesAlive++;

        if(currentWave == 0)
        {
            enemiesSpawnedInWave1++;
        }
        else if (currentWave == 1)
        {
            enemiesSpawnedInWave2++;
        }
        else if (currentWave == 2)
        {
            enemiesSpawnedInWave3++;
        }

        //StartCoroutine(ActivateGobSprite(enemy.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>()));
        WaveSpawning();
    }

    public IEnumerator ActivateGobSprite(SpriteRenderer sprite)
    {

        yield return new WaitForSeconds(0.2f);
        sprite.enabled = true;
    }

}
