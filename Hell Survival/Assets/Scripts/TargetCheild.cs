using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCheild : MonoBehaviour
{
    public GameObject parent;
    int mode, i;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mode = parent.GetComponent<TargetScript>().ModeChange;
        if (mode == 1 && i == 0)
        {
            gameObject.GetComponent<Animation>().Play("target_down");
            i++;
        }
    }
    
}
