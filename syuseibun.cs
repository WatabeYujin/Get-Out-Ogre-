using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class syuseibun : MonoBehaviour {

    public float attack=1000; //修正箇所・攻撃力
    float size;
    void Start()
    {
        size = transform.localScale.x;
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        size *= 1.05f;
        transform.localScale = new Vector3(size, size, size);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.SendMessage("Damage", attack);
        }
    }
}
