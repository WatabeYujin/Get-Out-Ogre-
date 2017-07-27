using UnityEngine;
using System.Collections;

public class Misobust : MonoBehaviour
{
    public int bulletcode;
    public float attack; //修正箇所・攻撃力
    public bool kinako;
    float time = 0;
	// Use this for initialization
	void Start () {
        if(!kinako)Destroy(gameObject, 0.3f);
        else Destroy(gameObject, 3f);
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
	}
    void OnTriggerEnter(Collider col)
    {
        if (!kinako&&col.gameObject.tag == "Enemy")
        {
            col.gameObject.SendMessage("Damage", attack);   //相手のDamage関数を実行する
            col.gameObject.SendMessage("bulletcode", bulletcode);
            //追記、第二引数に攻撃力をおくことでdamage関数に攻撃力を渡す
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (kinako && col.gameObject.tag == "Enemy" && time > 0.2f)
        {
            time = 0;
            col.gameObject.SendMessage("bulletcode", bulletcode);
            col.gameObject.SendMessage("Damage", attack);   //相手のDamage関数を実行する
            
        }
    }
}
