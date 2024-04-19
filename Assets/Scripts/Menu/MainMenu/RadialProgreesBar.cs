using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RadialProgreesBar : MonoBehaviour
{
    [SerializeField] public Image playImage;
    [SerializeField] public Image SelectorImage;
    [SerializeField] public Image settingsImage;
    [SerializeField] public Image quitImage;
    [SerializeField] private float fillSpeed = 0.5f;
    [SerializeField] private MenuManager menuManager;

    public float currentValue = 0f;
    public bool bActive = false;
    public int index = 0;

    // Update is called once per frame

    void Start()
    {
        playImage.fillAmount = 0;
        settingsImage.fillAmount = 0;
        quitImage.fillAmount = 0;
        SelectorImage.fillAmount = 0;
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
                        playImage.fillAmount = currentValue / 100;
                        break;
                    case 2:
                        settingsImage.fillAmount = currentValue / 100;
                        break;
                    case 3:
                        quitImage.fillAmount = currentValue / 100;
                        break;
                    case 1:
                        SelectorImage.fillAmount = currentValue / 100;
                        break;
                }
            } else if (currentValue >= 100)
            {
                switch (index)
                {
                    case 0:
                        menuManager.PlayButton();
                        currentValue = 0;
                        break;
                    case 2:
                         menuManager.SettingsButton();
                         currentValue = 0;
                        break;
                    case 3:
                        menuManager.QuitButton();
                        currentValue = 0;
                        break;
                    case 1:
                        menuManager.SelectorButton();
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
