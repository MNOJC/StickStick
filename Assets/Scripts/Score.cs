using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public float timeFirstStar;
    public float timeSecondStar;
    public float timeThirdStar;

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
        if (timeFinishedLevel <= timeThirdStar)
        {
            FirstStarImage.SetActive(true);
            SecondStarImage.SetActive(true);
            ThirdStarImage.SetActive(true);
        }
        else if (timeFinishedLevel <= timeSecondStar)
        {
            FirstStarImage.SetActive(true);
            SecondStarImage.SetActive(true);
            ThirdStarImage.SetActive(false);
        }
        else if (timeFinishedLevel <= timeFirstStar)
        {
            FirstStarImage.SetActive(true);
            SecondStarImage.SetActive(false);
            ThirdStarImage.SetActive(false);
        }
        else
        {
            FirstStarImage.SetActive(false);
            SecondStarImage.SetActive(false);
            ThirdStarImage.SetActive(false);
        }

        FirstText.text = timeFirstStar.ToString();
        SecondText.text = timeSecondStar.ToString();
        ThirdText.text = timeThirdStar.ToString();
    }         

        
}   
