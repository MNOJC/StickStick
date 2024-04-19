using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    //[SerializeField] private int[] starsPerLevel = new int[10]; // Tableau pour stocker les étoiles gagnées pour chaque niveau (index 0 à 9)
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
        // Vérifier si l'index de niveau est valide
        if (levelIndex >= 0 && levelIndex < Singleton.starsPerLevel.Length)
        {
            // Assurer que le nombre d'étoiles est compris entre 0 et 3
            Singleton.starsPerLevel[levelIndex] = Mathf.Clamp(starsNumber, 0, 3);
            // Sauvegarder la progression mise à jour
            SaveProgress();
            // Mettre à jour l'affichage des étoiles
        }
        else
        {
            Debug.LogWarning("Invalid level index: " + levelIndex);
        }
    }

    void SaveProgress()
    {
        // Convertir le tableau en chaîne
        string data = string.Join(",", Singleton.starsPerLevel);
        // Sauvegarder la chaîne dans PlayerPrefs
        PlayerPrefs.SetString("LevelProgress", data);
        PlayerPrefs.Save();
    }

    void LoadProgress()
    {
        if (PlayerPrefs.HasKey("LevelProgress"))
        {
            // Charger la chaîne depuis PlayerPrefs
            string data = PlayerPrefs.GetString("LevelProgress");
            // Convertir la chaîne en tableau d'entiers
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
