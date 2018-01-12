﻿using UnityEngine;
using UnityEngine.UI;

public class TimerGravity : MonoSingleton<TimerGravity>
{
    private const float TIMESPEED = 10.0f; // 10

    public float nrBalls;
    public bool checkTime;
    [Range(0f, 1f)]
    public float i;

    private Image timer;

    private void Start()
    {
        timer = GetComponent<Image>();
        nrBalls = 0f;
        i = 1;
        checkTime = false;
    }

    private void Update()
    {
        if (GameController.startTimerGravity)
        {
            checkTime = true;
            i -= Time.deltaTime / (TIMESPEED + nrBalls);
            timer.fillAmount = i;
            if (i < -0.03f)
            {
                i = 0;
                timer.fillAmount = 0;
                Ball.Instance.startFall = true;
                Ball.Instance.startFall = true;
            }
        }
        else if (!GameController.startTimerGravity && checkTime)
        {
            i += Time.deltaTime / 1.5f;
            timer.fillAmount = i;
            if (i > 1)
            {
                i = 1;
                Ball.Instance.startFall = false;
                Ball.Instance.startFall = false;
                checkTime = false;
                GameController.Instance.IsAllBallLanded();
                return;
            }
        }
    }

}
