using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RadialProgreesBar : MonoBehaviour
{
    [SerializeField] public Image playImage;
    [SerializeField] public Image settingsImage;
    [SerializeField] public Image quitImage;
    [SerializeField] private float fillSpeed = 0.5f;

    public float currentValue = 0f;
    public bool bActive = false;
    public int index = 0;

    // Update is called once per frame

    void Start()
    {
        playImage.fillAmount = 0;
        settingsImage.fillAmount = 0;
        quitImage.fillAmount = 0;
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
                    case 1:
                        settingsImage.fillAmount = currentValue / 100;
                        break;
                    case 2:
                        quitImage.fillAmount = currentValue / 100;
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
