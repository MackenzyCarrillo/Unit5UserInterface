using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button restartButton;
    public bool isGameActive;
    public List<GameObject> targets;
    public GameObject titleScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public GameObject pauseScreen;
    private bool paused;
    private float spawnRate = 1.0f;
    private int score;
    private int lives;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangePasued();
        }
    }

    IEnumerator SpawnTarget()
    {

        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }



    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score " + score;
    }

    public void LivesToAdd(int livesToAdd)
    {
        lives += livesToAdd;
        livesText.text = "Lives " + lives;

        if (lives <= 0)
        {
            GameOver();
        }
    }



    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGate()
    {


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 3;
        spawnRate = spawnRate /= difficulty;
        titleScreen.gameObject.SetActive(false);

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        LivesToAdd(3);
    }

    void ChangePasued()
    {
        if(!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    
    
    }





}