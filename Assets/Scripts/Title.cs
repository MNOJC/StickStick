using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] private Animator animatorTitle;

    void Start()
    { 
        AudioManager.instance.PlaySFX("Title");
        animatorTitle.SetTrigger("Title");
        Invoke("OpenMenu", 12f);
        Invoke("StartTheme", 4f);
    }

    void OpenMenu()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
    }

    void StartTheme()
    {
        AudioManager.instance.PlayMusic("Theme");
    }
}
