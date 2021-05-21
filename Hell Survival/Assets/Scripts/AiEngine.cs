using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AiEngine : MonoBehaviour
{
    public enum States { Follow, Attack, Death }
    public Transform Target, view;
    public States CurrentState;
    public float shootDistance = 25;
    bool Insight;
    public static bool dead;
    Vector3 directionToTarget;
    NavMeshAgent agent;
    Animator anim;
    static float hp;
    Vector3 WeaponToTarget;
    ParticleSystem muzzleflash;
    float Timeshoot;
    float everytime;
    int i = 1;
    AudioSource gunshot;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        muzzleflash = GetComponentInChildren<ParticleSystem>();
        everytime = 1.167f;
        gunshot = GetComponent<AudioSource>();
        hp = 1 * 3 * Gamply.Round / 2;
        dead = true;
        view = Target.Find("FollowMe");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStates();
        CheckForPlayer();
        if (Gamply.counterTime > 1)
        {
            Destroy(gameObject);
        }
    }
    void UpdateStates() // the different states of the IA
    {
        switch (CurrentState)
        {
            case States.Follow:
                Follow();
                break;
            case States.Attack:
                Attack();
                break;
            case States.Death:
                Death();
                break;

        }
    }
    void CheckForPlayer() // A Ray to check ecery second if the IA "can see" the player
    {
        directionToTarget = Target.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToTarget.normalized, out hit))
        {
            Insight = hit.transform.CompareTag("Player");
        }
    }
    void Follow()
    {
        if (directionToTarget.magnitude <= shootDistance && Insight) // if you see the player and you are in the shoot distance ==> attack
        {
            anim.SetBool("Isrunning", false);
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            CurrentState = States.Attack;
        }
        else // go to target
        {
            agent.SetDestination(Target.position);
            anim.SetBool("Isrunning", true);
        }
    }
    void Attack()
    {
        if (!Insight || Vector3.Distance(Target.position, transform.position) > shootDistance + 5)
        // if you can't see the player OR the distance betwwen you and the player is greater by 5 than the Shoot distance Follow him
        {
            agent.updatePosition = true;
            anim.SetBool("Isshooting", false);

            CurrentState = States.Follow;
        }
        else
        {
            agent.ResetPath();
            agent.updatePosition = false;
            anim.SetBool("Isshooting", true);
            transform.LookAt(view);
            if (Time.time - Timeshoot > everytime)
            {
                muzzleflash.Play();
                gunshot.Play();
                Timeshoot = Time.time;
                int rnd = Random.Range(1, 5);
                if (rnd < 3)
                {
                    PlayerMovment.hp--;
                }

            }
        }

    }
    public void Death()
    {
        agent.ResetPath();
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Destroy(gameObject, 1.45f);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            hp--;

            if (hp <= 0 && dead == true)
            {
                GetComponent<Collider>().enabled = false;
                Gamply.killcounter++;
                Gamply.Score++;
                anim.SetTrigger("Death");
                CurrentState = States.Death;
                dead = false;
            }
        }

    }
}

