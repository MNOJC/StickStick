using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;



public class SelectorMenuManager : MonoBehaviour
{
    private int index = 0;
    private float pressThreshold = 0.2f;
    private float holdThreshold = 1.7f;
    private float spaceKeyHeldStartTime = 0f;
    private float heldDuration = 0f;

    [SerializeField] private Image Level1Image;
    [SerializeField] private Image Level2Image;
    [SerializeField] private Image Level3Image;
    [SerializeField] private Image Level4Image;
    [SerializeField] private Image Level5Image;
    [SerializeField] private Image Level6Image;
    [SerializeField] private Image Level7Image;
    [SerializeField] private Image Level8Image;
    [SerializeField] private Image Level9Image;
    [SerializeField] private Image Level10Image;
    [SerializeField] private Button BackButton;

    [SerializeField] private SelectorRadialProgreesBar SelectorRadialProgreesBar;
    [SerializeField] private Animator MenuAnimator;
    [SerializeField] private Animator BackButtonAnimator;

    private LevelManager LevelManager;

    private void Start()
    {  
        LevelManager = FindObjectOfType<LevelManager>();
        BackButtonAnimator = BackButton.GetComponent<Animator>();
        if (Level1Image != null)
        {
            Level1Image.color = new Color(1f, 0.64f, 1f, 1f);
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
            SelectorRadialProgreesBar.SetActive(true);
        }
        else
        {
            SelectorRadialProgreesBar.SetActive(false);
            SelectorRadialProgreesBar.currentValue = 0;

        }
    }

    if (Input.GetKeyUp(KeyCode.Space))
    {
        SelectorRadialProgreesBar.SetActive(false);
        heldDuration = Time.time - spaceKeyHeldStartTime;
                   
        if (heldDuration < pressThreshold)
        {
            AudioManager.instance.PlaySFX("MenuSelected");
            ResetImageProgress();
            index = (index + 1) % 11;
            SelectorRadialProgreesBar.index = index;
            Debug.Log("Index: " + index);
            switch (index)
            {
                case 0:
                    Level1Image.color = new Color(1f, 0.64f, 1f, 1f);
                    Level2Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level3Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level4Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level5Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level6Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level7Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level8Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level9Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level10Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    BackButtonAnimator.SetBool("Selected", false);
                    break;
                case 1:
                    Level1Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level2Image.color = new Color(1f, 0.64f, 1f, 1f);
                    Level3Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level4Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level5Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level6Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level7Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level8Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level9Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level10Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    BackButtonAnimator.SetBool("Selected", false);
                    break;
                case 2:
                    Level1Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level2Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level3Image.color = new Color(1f, 0.64f, 1f, 1f);
                    Level4Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level5Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level6Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level7Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level8Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level9Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level10Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    BackButtonAnimator.SetBool("Selected", false);
                    break;
                case 3:
                    Level1Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level2Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level3Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level4Image.color = new Color(1f, 0.64f, 1f, 1f);
                    Level5Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level6Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level7Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level8Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level9Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level10Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    BackButtonAnimator.SetBool("Selected", false);
                    break;
                case 4:
                    Level1Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level2Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level3Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level4Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level5Image.color = new Color(1f, 0.64f, 1f, 1f);
                    Level6Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level7Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level8Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level9Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level10Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    BackButtonAnimator.SetBool("Selected", false);
                    break;
                case 5:
                    Level1Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level2Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level3Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level4Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level5Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level6Image.color = new Color(1f, 0.64f, 1f, 1f);
                    Level7Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level8Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level9Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level10Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    BackButtonAnimator.SetBool("Selected", false);
                    break;
                case 6:
                    Level1Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level2Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level3Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level4Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level5Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level6Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level7Image.color = new Color(1f, 0.64f, 1f, 1f);
                    Level8Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level9Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level10Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    BackButtonAnimator.SetBool("Selected", false);
                    break;
                case 7:
                    Level1Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level2Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level3Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level4Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level5Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level6Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level7Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level8Image.color = new Color(1f, 0.64f, 1f, 1f);
                    Level9Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level10Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    BackButtonAnimator.SetBool("Selected", false);
                    break;
                case 8:
                    Level1Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level2Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level3Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level4Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level5Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level6Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level7Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level8Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level9Image.color = new Color(1f, 0.64f, 1f, 1f);
                    Level10Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    BackButtonAnimator.SetBool("Selected", false);
                    break;
                case 9:
                    Level1Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level2Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level3Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level4Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level5Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level6Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level7Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level8Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level9Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level10Image.color = new Color(1f, 0.64f, 1f, 1f);
                    BackButtonAnimator.SetBool("Selected", false);
                    break;
                case 10:
                    Level1Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level2Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level3Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level4Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level5Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level6Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level7Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level8Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level9Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    Level10Image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    BackButtonAnimator.SetBool("Selected", true);
                    break;
            }
        }
        else if (heldDuration > holdThreshold)
        {
            switch (index)
            {
                case 0:
                    LevelManager.instance.currentLevelIndex = 0;
                    LoadScene("Scene_tuto_01");
                    break;
                case 1:
                    LevelManager.instance.currentLevelIndex = 1;
                    LoadScene("Scene_tuto_02");
                    break;
                case 2:
                    LevelManager.instance.currentLevelIndex = 2;
                    LoadScene("Scene_tuto_03");  
                    break;
                case 3:
                    LevelManager.instance.currentLevelIndex = 3;
                    LoadScene("Scene_tuto_04");
                    break;
                case 4:
                    LevelManager.instance.currentLevelIndex = 4;
                    LoadScene("Scene_05");
                    break;
                case 5:
                    LevelManager.instance.currentLevelIndex = 5;
                    LoadScene("Scene_06");
                    break;
                case 6:
                    LevelManager.instance.currentLevelIndex = 6;
                    LoadScene("Scene_07");
                    break;
                case 7:
                    LevelManager.instance.currentLevelIndex = 7;
                    LoadScene("Scene_08");
                    break;
                case 8:
                    LevelManager.instance.currentLevelIndex = 8;
                    LoadScene("Scene_09");
                    break;
                case 9:
                    LevelManager.instance.currentLevelIndex = 9;
                    LoadScene("Scene_10");
                    break;
                case 10:
                    BackButtonSelector();
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
    SelectorRadialProgreesBar.Level1Image.fillAmount = 0;
    SelectorRadialProgreesBar.Level2Image.fillAmount = 0;
    SelectorRadialProgreesBar.Level3Image.fillAmount = 0;
    SelectorRadialProgreesBar.Level4Image.fillAmount = 0;
    SelectorRadialProgreesBar.Level5Image.fillAmount = 0;
    SelectorRadialProgreesBar.Level6Image.fillAmount = 0;
    SelectorRadialProgreesBar.Level7Image.fillAmount = 0;
    SelectorRadialProgreesBar.Level8Image.fillAmount = 0;
    SelectorRadialProgreesBar.Level9Image.fillAmount = 0;
    SelectorRadialProgreesBar.Level10Image.fillAmount = 0;
    SelectorRadialProgreesBar.BackImage.fillAmount = 0;
}

public void BackButtonSelector()
{
    AudioManager.instance.PlaySFX("MenuEnter");
    LoadScene("MenuScene");
}

public void LoadScene(string sceneName)
{
    SceneManager.LoadScene(sceneName);

}
}
