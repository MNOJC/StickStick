using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedSingleton : MonoBehaviour
{
    public int[] starsPerLevel = new int[10];
    public static SavedSingleton instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("LevelProgress"))
        {
            string data = PlayerPrefs.GetString("LevelProgress");
            string[] values = data.Split(',');
            for (int i = 0; i < starsPerLevel.Length && i < values.Length; i++)
            {
                if (int.TryParse(values[i], out int stars))
                {
                    starsPerLevel[i] = Mathf.Clamp(stars, 0, 3);
                }
                else
                {
                    Debug.LogWarning("Failed to parse stars for level " + i);
                }
            }
        }
    }

    public void UpdateStars(int levelIndex, int stars)
    {
        if (levelIndex >= 0 && levelIndex < starsPerLevel.Length)
        {
            stars = Mathf.Clamp(stars, 0, 3);
            
            if (starsPerLevel[levelIndex] < stars)
            {
                starsPerLevel[levelIndex] = stars;
            }
        }
    }
}
