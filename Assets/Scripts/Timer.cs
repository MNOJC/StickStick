using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer;
    private bool bPauseTimer = true;

    [SerializeField]
    private TextMeshProUGUI firstMinute;

    [SerializeField]
    private TextMeshProUGUI secondMinute;

    [SerializeField]
    private TextMeshProUGUI separator;

    [SerializeField]
    private TextMeshProUGUI firstSecond;

    [SerializeField]
    private TextMeshProUGUI secondSecond;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if(!bPauseTimer)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
          
    }

    public void ResetTimer()
    {
        timer = 0;
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{0:00}{1:00}", minutes, seconds);

        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }
    
    public void PauseTimer()
    {
        bPauseTimer = true;
    }

    public void StartTimer()
    {
        bPauseTimer = false;
    }

    public void ResumeTimer()
    {
        bPauseTimer = false;
    }
}
