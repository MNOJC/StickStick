using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    private LevelManager levelManager;
    private Timer timer;
    private float levelFinishedTime;

    [SerializeField] private ParticleSystem levelEndParticles;



void Start() {
    levelManager = GameObject.Find("SceneManager").GetComponent<LevelManager>();
    timer = GameObject.Find("CanvaTimer").GetComponent<Timer>();

}
    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(levelEndParticles, transform.position, Quaternion.identity);
            levelFinishedTime = timer.timer;
            SetupLevelScore();
            timer.PauseTimer();
            
        }
    }
void SetupLevelScore()
{
    GameObject scoreCanva = GameObject.FindGameObjectWithTag("Score");
    GameObject childScoreCanva = scoreCanva.transform.GetChild(0).gameObject;
    

    LevelScoreData levelScoreData = FindObjectOfType<LevelScoreData>();
    Score score = FindObjectOfType<Score>();
    

    score.timeFirstStar = levelScoreData.timeFirstStar;
    score.timeSecondStar = levelScoreData.timeSecondStar;
    score.timeThirdStar = levelScoreData.timeThirdStar;

    childScoreCanva.SetActive(true);
    score.SetupScore(levelFinishedTime);  

    Invoke("LoadNextLevel", 6.0f);
    
}

private void LoadNextLevel()
{
    levelManager.LoadNextLevel();
}

}