using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNumber : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Timer timer;

    [SerializeField]
    private MainCharacter player;

    void Start()
    {
        animator.SetTrigger("LevelName");
        Invoke("StartLevel",3f);
    }

    void StartLevel()
    {
        timer.ResetTimer();
        player.DisableInput(false);
    }
}
