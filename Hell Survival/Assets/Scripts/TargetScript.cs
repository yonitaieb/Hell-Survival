using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

	
	public bool isHit = false, flip;
	public int i, ModeChange;
	int b, c, d, r, v;
	public GameObject[] flipTarget = new GameObject [5];
	public static int counter;
	public AudioClip upSound;
	public AudioClip downSound;
	public AudioSource audioSource;
	float timer;
    private void Start()
    {
		b = 0;
		c = 0;
		flip = true;

	}
    void Update ()
	{	
		if (isHit == true && b == 0) 
		{
			
			gameObject.GetComponent<Animation> ().Play("target_down");
			audioSource.GetComponent<AudioSource>().clip = downSound;
			audioSource.Play();
			i = 0;
			isHit = false;
			counter++;
			b++;
			v++;
			ModeChange = 1;
		}
		if (i == 1) transform.Rotate(0, 1, 0);
		if(i == 2)
        {
			if(flip == true)
            {
				if (d == 0)
				{
					r = Random.Range(0, 5);
					flipTarget[r].transform.localRotation = Quaternion.Euler(0, 0, 0);
					flip = false;
					timer = Time.time;
					d++;
				}
				if (Time.time - timer > 0.5f)
				{
					flipTarget[r].transform.localRotation = Quaternion.Euler(0, 180, 0);
					d--;
				}
			}
			if (Time.time - timer > 0.1 && v == 0) flip = true;
		}

		if (i == 3)
		{
            if (c == 0)
            {
                gameObject.transform.Translate(0.3f, 0, 0);
            }
            if (c == 1)
            {
                gameObject.transform.Translate(-0.3f, 0, 0);
            }
        }
		
	}
    private void OnCollisionEnter(Collision collision)
    {
		if (collision.collider.tag == "RightWall") c = 1;
		if (collision.collider.tag == "leftWall") c = 0;
        if (collision.collider.tag == "BackWall")
        {
            gameObject.SetActive(false);
        }


    }
}
