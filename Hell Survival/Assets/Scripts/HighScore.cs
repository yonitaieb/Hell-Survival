using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI Highscore , Score;
    public static int high;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Highscore.text = "Highscore: " + high.ToString();
        if(Score != null)
        {
            Score.text = "Score : " + Gamply.Score;
        }
    }
}
