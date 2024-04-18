using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] private Dictionary<int, int> levelsState = new (){
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 },
            { 5, 0 },
            { 6, 0 },
            { 7, 0 },
            { 8, 0 },
            { 9, 0 },
            { 10, 0 },
        };

    void Start()
    {
        UpdateStarLevelsImage();
    }
    void UpdateStarLevelsImage()
    {
        int index = 0;
        foreach (GameObject Level in levels)
        {
            GameObject stars = Level.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            

            for (int i = 0; i < levelsState[index + 1]; i++)
            {
                stars.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            index++;
        }
    }

    void UpdateLevelState(int levelIndex, int starsNumber) 
    {
       // levelsState[levelIndex] = starsNumber;
    }

}
