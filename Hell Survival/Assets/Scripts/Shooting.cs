using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public Transform bulletPrefab, casingPrefab;
    public Transform casingSpawnPoint, bulletSpawnPoint;
    public AudioClip shootSound, takeOutSound, holsterSound, reloadSoundOutOfAmmo, reloadSoundAmmoLeft, aimSound;            
    public AudioSource shootAudioSource, mainAudioSource;
    bool soundHasPlayed = false;
    public ParticleSystem sparkParticles, muzzleParticles;
    public GameObject[] mags = new GameObject[3];
    public float bulletForce = 400.0f;
    public static int i, t, c, shoots;
    public Text BulletCount;
    bool isAiming, realoding = true;
    int[] magval = new int[4] { 31, 31, 31, 31 };
    Animator anim;
    public Camera gunCamera;
    public float fovSpeed = 15.0f, defaultFov = 40.0f, timer, aimFov = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        shootAudioSource.clip = shootSound;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            anim.SetBool("Aim", true);
            gunCamera.fieldOfView = Mathf.Lerp(gunCamera.fieldOfView,aimFov, fovSpeed * Time.deltaTime);
            if (!soundHasPlayed)
            {
                mainAudioSource.clip = aimSound;
                mainAudioSource.Play();
                soundHasPlayed = true;
            }
        }
        else
        {
            gunCamera.fieldOfView = Mathf.Lerp(gunCamera.fieldOfView, defaultFov, fovSpeed * Time.deltaTime);
            isAiming = false;
            anim.SetBool("Aim", false);
            soundHasPlayed = false;
        }
        if (magval[t] < 1)
        {
            realoding = false;
            timer = Time.time;
            reload();
        }
        if (Input.GetMouseButtonDown(0) && realoding == true)
        {
            if (i < 4)
            {
                if (t == 0) magval[0]--;
                else if (t == 1) magval[1]--;
                else if (t == 2) magval[2]--;
                else if (t == 3) magval[3]--;
                sparkParticles.Emit(Random.Range(1 , 7));
                muzzleParticles.Emit(1);
                bulletInst();
                shootAudioSource.clip = shootSound;
                shootAudioSource.Play();
                shoots++;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            realoding = false;
            timer = Time.time;
            i--;
            reload();
        }
        BulletCount.text = magval[t].ToString() + "/31";
        if(Time.time - timer > 2) realoding = true;

        if (c == 5) 
        {
            for(int r = 0; r < magval.Length; r++)
            {
                magval[r] = 31;
            }
            for(int i = 0; i < mags.Length - 1; i++)
            {
                mags[i].SetActive(true);
            }
            c = 0;
            i = 0;
        }
    }
    void reload()
    {
        i++;
        if (t == 3) t = 0;
        if (i == 0)
        {
            t++;
            mainAudioSource.clip = reloadSoundAmmoLeft;
            mainAudioSource.Play();
            anim.Play("Reload Ammo Left", 0, 0);
        }
        else if (i == 1)
        {
            t++;
            mags[0].SetActive(false);
            mainAudioSource.clip = reloadSoundAmmoLeft;
            mainAudioSource.Play();
            anim.Play("Reload Ammo Left", 0, 0);
        }
        else if (i == 2)
        {
            t++;
            mags[1].SetActive(false);
            mainAudioSource.clip = reloadSoundAmmoLeft;
            mainAudioSource.Play();
            anim.Play("Reload Ammo Left", 0, 0);
        }
        else if (i == 3)
        {
            t++;
            mags[2].SetActive(false);
            mainAudioSource.clip = reloadSoundAmmoLeft;
            mainAudioSource.Play();
            anim.Play("Reload Ammo Left", 0, 0);
        }
        else if (i == 4)
        {
            mainAudioSource.clip = reloadSoundOutOfAmmo;
            mainAudioSource.Play();
        }
    }
    void bulletInst()
    {
        var bullet = (Transform)Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletForce;
        Instantiate(casingPrefab, casingSpawnPoint.transform.position, casingSpawnPoint.transform.rotation);
    }
    
}



