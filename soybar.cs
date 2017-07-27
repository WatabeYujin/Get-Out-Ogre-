using UnityEngine;
using System.Collections;

public class soybar : MonoBehaviour {
    public bool bar;
    public float attack; //修正箇所・攻撃力
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if(!bar)transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + 10, 0, 0);
	}
    void OnCollisionEnter(Collision col)
    {
        //void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.SendMessage("Damage", attack);
        }
    }
}
