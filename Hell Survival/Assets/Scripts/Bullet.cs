using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform metalImpactPrefab;

    private void Awake()
    {
        DestroyBullet();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "walkAble")
        {
            Instantiate(metalImpactPrefab, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
            Destroy(gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        if (collision.transform.tag == "Target")
        {
            collision.transform.gameObject.GetComponent<TargetScript>().isHit = true;
            Destroy(gameObject);
        }
        if (collision.transform.tag == "TargetAbsorb")
        {
            Destroy(gameObject);
        }
    }
    IEnumerable DestroyBullet()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
