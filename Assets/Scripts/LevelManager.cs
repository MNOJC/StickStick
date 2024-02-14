using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string[] levelNames;
    public MainCharacter mainCharacter;
    private static LevelManager instance;
    private int currentLevelIndex = 0;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // Sinon, fait de cette instance l'instance unique
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
