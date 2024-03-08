using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelNumber : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Timer timer;

    [SerializeField]
    private MainCharacter player;

    [SerializeField]
    private TextMeshProUGUI number;

    [SerializeField]
    private LevelScoreData data;

    void Start()
    {
        number.text = data.levelName;
        animator.SetTrigger("LevelName");
        Invoke("StartLevel",3f);
    }

    void StartLevel()
    {
        timer.ResetTimer();
        player.DisableInput(false);
        player.bGameStart = true;
    }
}
