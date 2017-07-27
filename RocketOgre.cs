using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketOgre : MonoBehaviour {
    float time;
    [SerializeField]
    int count;
    [SerializeField]
    AudioSource[] se;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= 1)
        {
            time = 0;
            count++;
            if (count<6)
            {
                se[count - 1].Play();
            }else
            {
                se[count - 1].Play();
                GetComponent<enemy>().speed = 5;
                Destroy(this);
            }
        }
	}
}
