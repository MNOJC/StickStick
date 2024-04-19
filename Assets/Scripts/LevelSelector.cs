using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    private SavedSingleton Singleton;
    

    void Start()
    {
        Singleton = FindObjectOfType<SavedSingleton>();
        SaveProgress();
        LoadProgress();
        UpdateStarLevelsImage();
    }

    void UpdateStarLevelsImage()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            GameObject stars = levels[i].transform.GetChild(0).GetChild(0).gameObject;

            for (int j = 0; j < Singleton.starsPerLevel[i]; j++)
            {
                stars.transform.GetChild(j).GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void UpdateLevelState(int levelIndex, int starsNumber)
    {
        if (levelIndex >= 0 && levelIndex < Singleton.starsPerLevel.Length)
        {
            Singleton.starsPerLevel[levelIndex] = Mathf.Clamp(starsNumber, 0, 3);
            SaveProgress();
        }
        else
        {
            Debug.LogWarning("Invalid level index: " + levelIndex);
        }
    }

    public void SaveProgress()
    {
        string data = string.Join(",", Singleton.starsPerLevel);
        PlayerPrefs.SetString("LevelProgress", data);
        PlayerPrefs.Save();
    }

    void LoadProgress()
    {
        if (PlayerPrefs.HasKey("LevelProgress"))
        {
            string data = PlayerPrefs.GetString("LevelProgress");
            string[] values = data.Split(',');
            for (int i = 0; i < Singleton.starsPerLevel.Length && i < values.Length; i++)
            {
                if (int.TryParse(values[i], out int stars))
                {
                    Singleton.starsPerLevel[i] = Mathf.Clamp(stars, 0, 3);
                }
                else
                {
                    Debug.LogWarning("Failed to parse stars for level " + i);
                }
            }
        }
    }
}
