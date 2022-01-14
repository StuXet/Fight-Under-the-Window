using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting };
    
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }


    public Wave[] waves;
    private int nextWave = 0;
    private int WaveNum = 1;

    public Transform[] SpawnPoints;
    public Text waveText;
    public Text waveCompletedText;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;
    public bool isGamePaused;
    public GameObject restartButton, quitButton, jab, uppercut, kick, block;

    void Start()
    {
        //waveCountdown = timeBetweenWaves;

        timeBetweenWaves = waveCountdown;
        PauseGame();

    }

     void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                waveCompletedText.gameObject.SetActive(true);
            }
            else
            {
                waveCompletedText.gameObject.SetActive(false);
                return;
            }
        }

        if (waveCountdown <=0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
        HandleWaveText();
        CheckPauseButton();
    }

    void WaveCompleted()
    {
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;
        if (nextWave +1 > waves.Length -1)
        {
            nextWave = 0;
            Debug.Log("Completed all waves");
        }
        WaveNum++;
        nextWave++;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }



    IEnumerator SpawnWave(Wave _wave)
    {
        
        Debug.Log("Spawning Wave");
        
        state = SpawnState.Spawning;
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1 / _wave.rate);
        }
        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        
        if (SpawnPoints.Length == 0)
        {
            Debug.LogError("No Enemy spawn points refrenced");
        }
        Transform _sp = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
        Debug.Log("Spawning Enemy");
    }

    void HandleWaveText()
    {
        waveText.text = WaveNum.ToString();
    }



    public void CheckPauseButton()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

    }
    public void PauseGame()
    {
        if (isGamePaused)
        {
            Debug.Log("unpuased");
            Time.timeScale = 1;
            isGamePaused = false;
            restartButton.SetActive(false);
            quitButton.SetActive(false);
            jab.SetActive(true);
            kick.SetActive(true);
            uppercut.SetActive(true);
            block.SetActive(true);

        }

        else
        {
            Time.timeScale = 0;
            isGamePaused = true;
            Debug.Log("paused");
            restartButton.SetActive(true);
            quitButton.SetActive(true);
            jab.SetActive(false);
            kick.SetActive(false);
            uppercut.SetActive(false);
            block.SetActive(false);
        }

    }
    public void RestartBottun()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitBottun()
    {
        Application.Quit();
        Debug.Log("Quiting Game!");
    }

    
}
