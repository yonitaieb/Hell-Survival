using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Gamply : MonoBehaviour
{
    public GameObject ammoBox, roundStart;
    public GameObject[] pos;
    public GameObject[] Enemypos;
    public Slider HealthBar;
    public Text time;
    public AudioSource main , soundtrack;
    public AudioClip getReady, forest;
    public TextMeshProUGUI countDown;
    float newBox = 60;
    float timer, counter;
    int ammoTime = 60;
    bool playSound;
    public static int counterTime = 4 , i = 10;
    public static int killcounter;
    public static int Round = 1;
    public Text theRound;
    public static int Score;
    public int ex;
    public Text theScore;
    float rounder = 30 * (Round * 0.5f);
    public GameObject Enemy;
    private void Start()
    {
        InvokeRepeating("CreateNewEnemy", 0, 5f);
        killcounter = 0;
        playSound = true;
    }
    void Update()
    {
        if(soundtrack.isPlaying == false) soundtrack.PlayOneShot(forest);
        
        if(PlayerMovment.hp < i)
        {
            HealthBar.value -= 0.1f;
            i = PlayerMovment.hp;
        }
        if (killcounter == rounder && AiEngine.dead == true)
        {
            killcounter = 0;
            Round++;
            roundStart.SetActive(true);
            main.PlayOneShot(getReady);
            counterTime = 4;
        }
        theScore.text = "Score : " + Score;
        theRound.text = "Round : " + Round;
        if (Time.time - timer > newBox)
        {
            timer = Time.time;
            ammoTime = 60;
            newAmmoBox();
        }
        if (Time.time - counter > 1)
        {
            counter = Time.time;
            ammoTime--;
            counterTime--;
        }
        if (counterTime == 0) roundStart.SetActive(false);
        if (counterTime > 1 && playSound == true)
        {
            playSound = false;
            main.PlayOneShot(getReady);
        }
        countDown.text = "" + counterTime.ToString();
        time.text = "Ammo drop in: " + ammoTime.ToString();

    }
    void newAmmoBox()
    {
        int x = Random.Range(0, 8);
        Vector3 AmmoPos = pos[x].transform.position;
        Instantiate(ammoBox);
        ammoBox.transform.position = AmmoPos;
    }
    public void CreateNewEnemy()
    {
        int x = Random.Range(0, 9);
        Vector3 EnemyPosition = Enemypos[x].transform.position;
        Instantiate(Enemy);
        Enemy.transform.position = EnemyPosition;
    }
}
