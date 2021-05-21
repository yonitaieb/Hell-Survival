using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


public class PlayerMovment : MonoBehaviour
{
    public float mouseSensitivety = 100;
    public GameObject rifle, handgun;
    public AudioClip walkingSound, runningSound, modeSet;
    public AudioSource audioSource;
    public float walk = 3;
    int walking, running, playSound;
    public bool inGround;
    float Xrotation = 0;
    public Transform view;
    float mouseY, mouseX;
    public static bool usingRifle;
    public static int hp = 10 , curserFree;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = walkingSound;
        audioSource.loop = true;
        handgun.SetActive(false);
        usingRifle = true;
    }

    void Update()
    {
        if (hp <= 0)
        {
            if (HighScore.high < Gamply.Score)
            {
                HighScore.high = Gamply.Score;
            }
            curserFree = 1;
            SceneManager.LoadScene("lose");
        }
        if (usingRifle == true)
        {
            handgun.transform.position = rifle.transform.position;
            handgun.transform.localRotation = rifle.transform.localRotation;
        }
        if (usingRifle == false)
        {
            rifle.transform.position = handgun.transform.position;
            rifle.transform.localRotation = handgun.transform.localRotation;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            rifle.SetActive(false);
            handgun.SetActive(true);
            usingRifle = false;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            rifle.SetActive(true);
            handgun.SetActive(false);
            usingRifle = true;
        }

        mouseX = Input.GetAxis("Mouse X") * mouseSensitivety * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);

        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivety * Time.deltaTime;
        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation, -80, 80);
        view.localRotation = Quaternion.Euler(Xrotation, 0, 0);

        audioMaker();
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, 10 * walk * Time.deltaTime);
            walking = 1;
            audioMaker();
        }
        else walking = 0;
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -10 * walk * Time.deltaTime);
            walking = 1;
            audioMaker();
        }
        else walking = 0;
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(10 * walk * Time.deltaTime, 0, 0);
            walking = 1;
            audioMaker();
        }
        else walking = 0;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-10 * walk * Time.deltaTime, 0, 0);
            walking = 1;
            audioMaker();
        }
        else walking = 0;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walk = 4;
            running = 1;
            walking = 1;
            audioMaker();
        }
        else
        {
            walk = 3;
            running = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && inGround == true)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(0, 109000 * Time.deltaTime, 0);
            inGround = false;
        }
        if (Input.GetKey(KeyCode.LeftControl)) gameObject.GetComponent<CapsuleCollider>().height = 2.05f;
        else gameObject.GetComponent<CapsuleCollider>().height = 4.1f;
        if (curserFree == 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
    void audioMaker()
    {
        if (walking != 0)
        {
            if (!audioSource.isPlaying) audioSource.Play();
        }
        else if (audioSource.isPlaying) audioSource.Pause();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "walkAble") inGround = true;
        else if (collision.collider.tag != "walkAble") inGround = false;
        if (collision.collider.tag == "Ammo")
        {
            Shooting.c = 5;
            HandGunShooting.c = 5;
        }
    }
}

