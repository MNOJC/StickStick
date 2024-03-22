using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;



public class MenuManager : MonoBehaviour
{
    private int index = 0;
    private float pressThreshold = 0.2f;
    private float holdThreshold = 1.7f;
    private float spaceKeyHeldStartTime = 0f;
    private float heldDuration = 0f;

    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private RadialProgreesBar radialProgreesBar;
    private Animator PlayButtonAnimator;
    private Animator SettingsButtonAnimator;
    private Animator QuitButtonAnimator;

    private void Start()
    {  
        SettingsButtonAnimator = settingsButton.GetComponent<Animator>();
        QuitButtonAnimator = quitButton.GetComponent<Animator>();
        PlayButtonAnimator = playButton.GetComponent<Animator>();
        if (PlayButtonAnimator != null)
        {
            PlayButtonAnimator.SetBool("Selected", true);
        }
    
       
    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        
        spaceKeyHeldStartTime = Time.time;
        
    }

    if (Input.GetKey(KeyCode.Space))
    {
        heldDuration = Time.time - spaceKeyHeldStartTime;
        if (heldDuration > pressThreshold)
        {
            radialProgreesBar.SetActive(true);
        }
        else
        {
            radialProgreesBar.SetActive(false);
            radialProgreesBar.currentValue = 0;

        }
    }

    if (Input.GetKeyUp(KeyCode.Space))
    {
        radialProgreesBar.SetActive(false);
        heldDuration = Time.time - spaceKeyHeldStartTime;
                   
        if (heldDuration < pressThreshold)
        {
            ResetImageProgress();
            index = (index + 1) % 3;
            radialProgreesBar.index = index;
            Debug.Log("Index: " + index);
            switch (index)
            {
                case 0:

                        PlayButtonAnimator.SetBool("Selected", true);
                        SettingsButtonAnimator.SetBool("Selected", false);
                        QuitButtonAnimator.SetBool("Selected", false);
                    
                    break;
                case 1:

                        SettingsButtonAnimator.SetBool("Selected", true);
                        PlayButtonAnimator.SetBool("Selected", false);
                        QuitButtonAnimator.SetBool("Selected", false);

                        break;
                case 2:

                        QuitButtonAnimator.SetBool("Selected", true);
                        PlayButtonAnimator.SetBool("Selected", false);
                        SettingsButtonAnimator.SetBool("Selected", false);
                    break;
            }
        }
        else if (heldDuration > holdThreshold)
        {
            switch (index)
            {
                case 0:
                    SceneManager.LoadScene("Scene_01");
                    break;
                case 1:
                    Debug.Log("Settings Button Pressed");
                    break;
                case 2:
                    Application.Quit();
                    break;
            }
        }   
        else if (heldDuration < holdThreshold)
        {
           ResetImageProgress();
        }   
        
    }
}
void ResetImageProgress()
{
    radialProgreesBar.playImage.fillAmount = 0;
    radialProgreesBar.settingsImage.fillAmount = 0;
    radialProgreesBar.quitImage.fillAmount = 0;
}

}
