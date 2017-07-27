using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aburaage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Enemy")
		{
			col.gameObject.SendMessage("Damage", 1000);
			Destroy (transform.root.gameObject);
		}
	}
}
