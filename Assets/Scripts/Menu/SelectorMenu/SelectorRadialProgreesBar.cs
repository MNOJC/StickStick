using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectorRadialProgreesBar : MonoBehaviour
{
    [SerializeField] public Image Level1Image;
    [SerializeField] public Image Level2Image;
    [SerializeField] public Image Level3Image;
    [SerializeField] public Image Level4Image;
    [SerializeField] public Image Level5Image;
    [SerializeField] public Image Level6Image;
    [SerializeField] public Image Level7Image;
    [SerializeField] public Image Level8Image;
    [SerializeField] public Image Level9Image;
    [SerializeField] public Image Level10Image;
    [SerializeField] public Image BackImage;
    
    [SerializeField] private float fillSpeed = 0.5f;
    [SerializeField] private SelectorMenuManager SelectorMenuManager;

    public float currentValue = 0f;
    public bool bActive = false;
    public int index = 0;
    private LevelManager LevelManager;

    // Update is called once per frame

    void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();
        Level1Image.fillAmount = 0;
        Level2Image.fillAmount = 0;
        Level3Image.fillAmount = 0;
        Level4Image.fillAmount = 0;
        Level5Image.fillAmount = 0;
        Level6Image.fillAmount = 0;
        Level7Image.fillAmount = 0;
        Level8Image.fillAmount = 0;
        Level9Image.fillAmount = 0;
        Level10Image.fillAmount = 0;
        BackImage.fillAmount = 0;

    }
    void Update()
    {
    
        if(bActive)
        {
            if (currentValue < 100)
            {
                currentValue += fillSpeed * Time.deltaTime;
                switch (index)
                {
                    case 0:
                        Level1Image.fillAmount = currentValue / 100;
                        break;
                    case 1:
                        Level2Image.fillAmount = currentValue / 100;
                        break;
                    case 2:
                        Level3Image.fillAmount = currentValue / 100;
                        break;
                    case 3:
                        Level4Image.fillAmount = currentValue / 100;
                        break;
                    case 4:
                        Level5Image.fillAmount = currentValue / 100;
                        break;
                    case 5:
                        Level6Image.fillAmount = currentValue / 100;
                        break;
                    case 6:
                        Level7Image.fillAmount = currentValue / 100;
                        break;
                    case 7:
                        Level8Image.fillAmount = currentValue / 100;
                        break;
                    case 8:
                        Level9Image.fillAmount = currentValue / 100;
                        break;
                    case 9:
                        Level10Image.fillAmount = currentValue / 100;
                        break;
                    case 10:
                        BackImage.fillAmount = currentValue / 100;
                        break;
                        
                }
            } else if (currentValue >= 100)
            {
                switch (index)
                {
                    case 0:
                        LevelManager.instance.currentLevelIndex = 0;
                        SelectorMenuManager.LoadScene("Scene_tuto_01");
                        currentValue = 0;
                        break;
                    case 1:
                        LevelManager.instance.currentLevelIndex = 1;
                        SelectorMenuManager.LoadScene("Scene_tuto_02");
                        currentValue = 0;
                        break;
                    case 2:
                        LevelManager.instance.currentLevelIndex = 2;
                        SelectorMenuManager.LoadScene("Scene_tuto_03");
                        currentValue = 0;
                        break;
                    case 3:
                        LevelManager.instance.currentLevelIndex = 3;
                        SelectorMenuManager.LoadScene("Scene_tuto_04");
                        currentValue = 0;
                        break;
                    case 4:
                        LevelManager.instance.currentLevelIndex = 4;
                        SelectorMenuManager.LoadScene("Scene_05");
                        currentValue = 0;
                        break;
                    case 5:
                        LevelManager.instance.currentLevelIndex = 5;
                        SelectorMenuManager.LoadScene("Scene_06");
                        currentValue = 0;
                        break;
                    case 6:
                        LevelManager.instance.currentLevelIndex = 6;
                        SelectorMenuManager.LoadScene("Scene_07");
                        currentValue = 0;
                        break;
                    case 7:
                        LevelManager.instance.currentLevelIndex = 7;
                        SelectorMenuManager.LoadScene("Scene_08");
                        currentValue = 0;
                        break;
                    case 8:
                        LevelManager.instance.currentLevelIndex = 8;
                        SelectorMenuManager.LoadScene("Scene_09");
                        currentValue = 0;
                        break;
                    case 9:
                        LevelManager.instance.currentLevelIndex = 9;
                        SelectorMenuManager.LoadScene("Scene_10");
                        currentValue = 0;
                        break;
                    case 10:
                        SelectorMenuManager.BackButtonSelector();
                        currentValue = 0;
                        break;
                }
                
            }
        } 
        else
        {
            currentValue = 0;
        }
    }

    public void SetActive(bool active)
    {
        bActive = active;
    }
       
}
