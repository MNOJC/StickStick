using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;



public class SettingsMenuManager : MonoBehaviour
{
    private int index = 0;
    private float pressThreshold = 0.2f;
    private float holdThreshold = 1.7f;
    private float spaceKeyHeldStartTime = 0f;
    private float heldDuration = 0f;

    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private SettingsRadialProgressBar radialProgreesBar;
    [SerializeField] private Animator MenuAnimator;
    private Animator PlayButtonAnimator;
    private Animator SettingsButtonAnimator;
    private Animator QuitButtonAnimator;
    public bool canInteract = true;
    

    private void Start()
    {  
        MenuAnimator.SetTrigger("FadeIn");
        SettingsButtonAnimator = settingsButton.GetComponent<Animator>();
        QuitButtonAnimator = quitButton.GetComponent<Animator>();
        PlayButtonAnimator = playButton.GetComponent<Animator>();
        if (PlayButtonAnimator != null)
        {
            PlayButtonAnimator.SetBool("SelectedSettings", true);
        }
    
       
    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        canInteract = true;
        spaceKeyHeldStartTime = Time.time;
        
    }

    if (Input.GetKey(KeyCode.Space))
    {
        if(canInteract)
        {

        
        heldDuration = Time.time - spaceKeyHeldStartTime;
        if (heldDuration > pressThreshold && canInteract)
        {
            radialProgreesBar.SetActive(true);
        }
        else
        {
            radialProgreesBar.SetActive(false);
            radialProgreesBar.currentValue = 0;

        }
        }
    }

    if (Input.GetKeyUp(KeyCode.Space))
    {
        if (canInteract)
        {           
        radialProgreesBar.SetActive(false);
        heldDuration = Time.time - spaceKeyHeldStartTime;
                   
        if (heldDuration < pressThreshold)
        {
            AudioManager.instance.PlaySFX("MenuSelected");
            ResetImageProgress();
            index = (index + 1) % 3;
            radialProgreesBar.index = index;
            Debug.Log("Index: " + index);
            switch (index)
            {
                case 0:

                        PlayButtonAnimator.SetBool("SelectedSettings", true);
                        SettingsButtonAnimator.SetBool("Selected", false);
                        QuitButtonAnimator.SetBool("Selected", false);
                    
                    break;
                case 1:

                        SettingsButtonAnimator.SetBool("Selected", true);
                        PlayButtonAnimator.SetBool("SelectedSettings", false);
                        QuitButtonAnimator.SetBool("Selected", false);

                        break;
                case 2:

                        QuitButtonAnimator.SetBool("Selected", true);
                        PlayButtonAnimator.SetBool("SelectedSettings", false);
                        SettingsButtonAnimator.SetBool("Selected", false);
                    break;
            }
        }
        else if (heldDuration > holdThreshold)
        {
            switch (index)
            {
                case 0:
                    ToggleMusic();
                    canInteract = false;
                    break;
                case 1:
                    ToggleSFX();
                    canInteract = false;
                    break;
                case 2:
                    Back();
                    canInteract = false;
                    break;
            }
            
        }   
        else if (heldDuration < holdThreshold)
        {
           ResetImageProgress();
        }   
        }
    }
}
public void ResetImageProgress()
{
    radialProgreesBar.playImage.fillAmount = 0;
    radialProgreesBar.settingsImage.fillAmount = 0;
    radialProgreesBar.quitImage.fillAmount = 0;
}

public void ToggleMusic()
{
    AudioManager.instance.ToggleMusic();
    AudioManager.instance.PlaySFX("MenuEnter");
}

public void ToggleSFX()
{
    AudioManager.instance.ToggleSFX();
    AudioManager.instance.PlaySFX("MenuEnter");
}

public void Back()
{
    MenuAnimator.SetTrigger("FadeOut");
    StartCoroutine(LoadMenu());
    AudioManager.instance.PlaySFX("MenuEnter");
}

IEnumerator LoadMenu()
{
    yield return new WaitForSeconds(0f);
    SceneManager.LoadScene("MenuScene");

}
}


