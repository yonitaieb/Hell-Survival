using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnMangar : MonoBehaviour
{
   public void startSurvival()
   {
        SceneManager.LoadScene(1);
        PlayerMovment.curserFree = 0;
        PlayerMovment.hp = 10;
        Gamply.counterTime = 4;
        Gamply.Score = 0;
   }
    public void Home()
    {
        SceneManager.LoadScene("Start");
        PlayerMovment.hp = 10;
        PlayerMovment.curserFree = 1;
    }
    public void restartRange()
    {
        SceneManager.LoadScene("Range");
        Shooting.shoots = 0;
        TargetScript.counter = 0;
        PlayerMovment.curserFree = 0;
        PlayerMovment.hp = 10;
    }
}
