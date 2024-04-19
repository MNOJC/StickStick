using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string[] levelNames;
    private MainCharacter mainCharacter;
    public static LevelManager instance;
    public int currentLevelIndex = 0;

    void Awake()
    {
        //mainCharacter = GameObject.Find("Player").GetComponent<MainCharacter>();
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
            SceneManager.LoadScene("MainMenu");
            currentLevelIndex = 0;
        }
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        LoadCurrentLevel();
    }

}
