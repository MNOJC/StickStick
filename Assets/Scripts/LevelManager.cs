using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string[] levelNames;
    private MainCharacter mainCharacter;
    private static LevelManager instance;
    private int currentLevelIndex = 0;

    void Awake()
    {
        mainCharacter = GameObject.Find("Player").GetComponent<MainCharacter>();
    }
void Start()
{
    if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    else
    {
       DestroyImmediate(this.gameObject);
    }
}

    public void LoadCurrentLevel()
    {
        if (currentLevelIndex < levelNames.Length)
        {
            Debug.Log("LINDEX CCC :" + currentLevelIndex);
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
