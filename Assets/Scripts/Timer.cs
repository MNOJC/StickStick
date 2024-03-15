using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer;
    private bool bPauseTimer = true;

    [SerializeField]
    private TextMeshProUGUI firstMillisecond;

    [SerializeField]
    private TextMeshProUGUI secondMillisecond;

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
        float seconds = Mathf.FloorToInt(time);
        float milliseconds = Mathf.FloorToInt((time - seconds) * 1000);

        string currentTime = string.Format("{0:00}{1:00}", seconds, milliseconds);

        firstSecond.text = currentTime[0].ToString();
        secondSecond.text = currentTime[1].ToString();
        firstMillisecond.text = currentTime[2].ToString();
        secondMillisecond.text = currentTime[3].ToString();
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
