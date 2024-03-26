using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelNumber : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private Timer timer;
    private MainCharacter player;

    [SerializeField]
    private TextMeshProUGUI number;
    private LevelScoreData data;


    void Awake()
    {
        GameObject canvasTimerObject = GameObject.Find("CanvaTimer");
        GameObject playerObject = GameObject.Find("Player");
        GameObject LevelScoreDataObject = GameObject.Find("LevelScoreManager");

        timer = canvasTimerObject.GetComponent<Timer>();
        player = playerObject.GetComponent<MainCharacter>();
        data = LevelScoreDataObject.GetComponent<LevelScoreData>();
    }
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
