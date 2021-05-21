using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeScore : MonoBehaviour
{
    float timer, i , timecounter, d;
    int NowTime;
    public Text grade, say, time, shots, Hits;
    public GameObject panelEnd, playerRifle, playerHandgun;
    void Start()
    {
        i = 0;
        NowTime = 0;
        d = 0;
        panelEnd.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (i == 0)
        {
            timer = Time.time;
            timecounter = Time.time;
            i++;
        }
        if(Time.time - timecounter > 1)
        {
            NowTime++;
            timecounter = Time.time;
        }
        time.text = "Time: " + NowTime.ToString();
        shots.text = "Shots: " + Shooting.shoots.ToString();
        Hits.text = "Hits: " + TargetScript.counter.ToString();
        if(d == 1) Cursor.lockState = CursorLockMode.None;
        if (TargetScript.counter == 42 && Time.time - timer < 40 && Shooting.shoots == 42 && d == 0)
        {
            grade.text = "Grade: 100!";
            say.text = "Elite Level";
            panelEnd.SetActive(true);
            playerHandgun.SetActive(false);
            playerRifle.SetActive(false);
            PlayerMovment.curserFree = 1;
            Cursor.visible = true;
            d++;
        }
        else if(TargetScript.counter == 42 && Time.time - timer < 50 && Shooting.shoots <= 50 && d == 0)
        {
            grade.text = "Grade: 80!";
            say.text = "Commando Level";
            panelEnd.SetActive(true);
            playerHandgun.SetActive(false);
            playerRifle.SetActive(false);
            PlayerMovment.curserFree = 1;
            Cursor.visible = true;
            d++;
        }
        else if (TargetScript.counter == 42 && Time.time - timer < 60 && Shooting.shoots <= 60 && d == 0)
        {
            grade.text = "Grade: 60!";
            say.text = "Soldier Level";
            panelEnd.SetActive(true);
            playerHandgun.SetActive(false);
            playerRifle.SetActive(false);
            PlayerMovment.curserFree = 1;
            Cursor.visible = true;
            d++;
        }
        else if (TargetScript.counter == 42 && d == 0 && Shooting.shoots >= 60 || TargetScript.counter == 42 && d == 0 && Time.time - timer > 60)
        {
            grade.text = "Grade: 40!";
            say.text = "Rookie Level";
            panelEnd.SetActive(true);
            playerHandgun.SetActive(false);
            playerRifle.SetActive(false);
            PlayerMovment.curserFree = 1;
            Cursor.visible = true;
            d++;
        }
    }
}
