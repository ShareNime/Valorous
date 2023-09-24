using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { WAITING, COUNTING, SPAWNING, FINISHED}
    [System.Serializable]
    public class wave
    {
        public string name;
        public GameObject enemy;
        public int count;
        public float rate;
    }
    public wave[] waves;
    private int nextwaves = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    private float searchCountdown = 1f;
    public SpawnState state = SpawnState.COUNTING;
    public TextMeshProUGUI enemyLeft;
    public TextMeshProUGUI waitTime;
    
    private void Awake()
    {
       
    }
    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        FindObjectOfType<AudioManager>().Play("lagu");
        FindObjectOfType<AudioManager>().Play("ambient");
        

    }
    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            //Check if Enemy is Still alive
            if (!EnemyIsAlive())
            {
                //Begin a New Round
                waveCompleted();

            }
            else
            {
                enemyLeft.text = "Enemy Left:" + GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();
                return;
            }
        }
        if(state == SpawnState.FINISHED)
        {
            return;
        }
        if(waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextwaves]));
                waitTime.text = "";
                //start spawning waves;
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
            int temp = (int)waveCountdown;
            waitTime.text = temp.ToString();
        }
    }
    void waveCompleted()
    {
        Debug.Log("Waves Completed");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if(nextwaves +1 > waves.Length - 1)
        {
            //STATE WHEN GAME IS COMPLETED
            
            //nextwaves = 0; // THIS CAUSE LOOP
            state = SpawnState.FINISHED; //KALAU MAU END
            Debug.Log("ALL WAVES COMPLETED");

            FindObjectOfType<GameManager>().WinGameActive();
            
        }
        else
        {
            nextwaves++;
        }
    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        
            return true;
    }
    IEnumerator SpawnWave(wave _wave)
    {
        state = SpawnState.SPAWNING;
        //spawning
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING;
        yield break;
    }
    void SpawnEnemy(GameObject _enemy1)
    {
        //Spawn Enemy
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        
           
        
        
            Instantiate(_enemy1, _sp.position, _sp.rotation);
        
        Debug.Log("Spawning Enemy" + _enemy1.name);
    }
}
    
