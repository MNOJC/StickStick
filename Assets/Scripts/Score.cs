using System.Collections;
using System.Collections.Generic;
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

    public void SetupScore(float timeFinishedLevel)
    {
        StartCoroutine(VictorySound());
        animator.SetTrigger("Score");

        if (timeFinishedLevel <= timeFirstStar)
        {
            StartCoroutine(WaitForSeconds(3f, FirstStarImage, "FirstStar"));
        }
         if (timeFinishedLevel <= timeSecondStar)
        {
            StartCoroutine(WaitForSeconds(4f, SecondStarImage, "SecondStar"));
        }
         if (timeFinishedLevel <= timeThirdStar)
        {
            StartCoroutine(WaitForSeconds(5f, ThirdStarImage, "ThirdStar"));
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
