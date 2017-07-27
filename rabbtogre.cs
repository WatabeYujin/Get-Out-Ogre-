using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rabbtogre : MonoBehaviour {
    [SerializeField]
    Rigidbody ri;
    float hoprate = 1.8f;
	// Use this for initialization
	void Start () {
        ri = GetComponent<Rigidbody>();
        StartCoroutine("hop");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator hop()
    {
        while (true)
        {
            yield return new WaitForSeconds(hoprate);
            ri.AddForce(transform.right * Random.Range(-12f, 12.1f),ForceMode.VelocityChange);
            ri.AddForce(transform.up * 3, ForceMode.VelocityChange);
        }
    }
}
