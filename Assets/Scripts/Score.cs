using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public float timeFirstStar;
    public float timeSecondStar;
    public float timeThirdStar;
    public ParticleSystem StarParticle;

    public Animator animator;

    [SerializeField]
    private GameObject FirstStarImage;

    [SerializeField]
    private GameObject SecondStarImage;

    [SerializeField]
    private GameObject ThirdStarImage;

    [SerializeField]
    private TextMeshProUGUI FirstText;

    [SerializeField]
    private TextMeshProUGUI SecondText;

    [SerializeField]
    private TextMeshProUGUI ThirdText;

    private LevelManager LevelManager;
    private SavedSingleton Singleton;

    private bool canQuit = false;

    private float spaceKeyHeldStartTime = 0f;
    private float heldDuration = 0f;


    private void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();
        Singleton = FindObjectOfType<SavedSingleton>();
    }

    void Update()
    {
        if(canQuit)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
        
                spaceKeyHeldStartTime = Time.time;
        
            }

        if (Input.GetKey(KeyCode.Space))
        {
            heldDuration = Time.time - spaceKeyHeldStartTime;
            if (heldDuration > 1.5f)
            {
                SceneManager.LoadScene("MenuScene");
                LevelManager.currentLevelIndex = 0;
            }

        }
        }
    }
    public void SetupScore(float timeFinishedLevel)
    {
        canQuit = true;
        StartCoroutine(VictorySound());
        animator.SetTrigger("Score");

        if (timeFinishedLevel <= timeFirstStar)
        {
            StartCoroutine(WaitForSeconds(3f, FirstStarImage, "FirstStar"));
            SavedSingleton.instance.UpdateStars(LevelManager.currentLevelIndex, 1);
            Debug.Log(LevelManager.instance.currentLevelIndex);
        }
         if (timeFinishedLevel <= timeSecondStar)
        {
            StartCoroutine(WaitForSeconds(4f, SecondStarImage, "SecondStar"));
            SavedSingleton.instance.UpdateStars(LevelManager.currentLevelIndex, 2);
        }
         if (timeFinishedLevel <= timeThirdStar)
        {
            StartCoroutine(WaitForSeconds(5f, ThirdStarImage, "ThirdStar"));
            SavedSingleton.instance.UpdateStars(LevelManager.currentLevelIndex, 3);
        }

        FirstText.text = "< " + timeFirstStar.ToString() + "s";
        SecondText.text = "< " + timeSecondStar.ToString() + "s";
        ThirdText.text = "< " + timeThirdStar.ToString() + "s";
    }         

        IEnumerator WaitForSeconds(float time, GameObject starImage, string star)
        {
            yield return new WaitForSeconds(time);
            starImage.SetActive(true);
            animator.SetTrigger(star);

            
            yield return new WaitForSeconds(0.7f);
            Instantiate(StarParticle, starImage.transform.position, Quaternion.identity);
            AudioManager.instance.PlaySFX("Star");

        }

        IEnumerator VictorySound()
        {
            yield return new WaitForSeconds(1.0f);
            AudioManager.instance.PlaySFX("Victory");
        }
}   
