using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxEngine : MonoBehaviour
{
    Animator anim;
    RigidbodyConstraints rigidbodyconstrains;
    float timer;
    public int i;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        timer = 4;

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - timer > 3 && Time.time - timer < 4 && i == 0) Destroy(gameObject);
        if (transform.position.y < 0) transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            anim.SetTrigger("Open");
            timer = Time.time;
        }
        if (collision.collider.tag == "walkAble")
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

}
