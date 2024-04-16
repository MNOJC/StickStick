using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] private Animator animatorTitle;

    void Start()
    {
        animatorTitle.SetTrigger("Title");
        Invoke("OpenMenu", 12f);
    }

    void OpenMenu()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
    }
}
