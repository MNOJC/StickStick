using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
     public string[] levelNames;

    private int currentLevelIndex = 0;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadCurrentLevel()
    {
        if (currentLevelIndex < levelNames.Length)
        {
            SceneManager.LoadScene(levelNames[currentLevelIndex]);
        }
        else
        {
            Debug.Log("Tous les niveaux ont été terminés!");
        }
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        LoadCurrentLevel();
    }

}
