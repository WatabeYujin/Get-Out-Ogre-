using UnityEngine;
using System.Collections;

public class rifleBullet : MonoBehaviour {
    private shot Shot;
    private int bulletcode;
	public float attack; //修正箇所・攻撃力
    bool destroy = false;
    float size;
    public GameObject misobust;
    public bool miso = false;
    public bool kinako = false;
    public bool Throug = false;
    public bool kuromame;
    int hit;
	void Start(){
        size = transform.localScale.x;
        StartCoroutine("sru");
	}
    IEnumerator sru()
    {
        yield return new WaitForSeconds(0.01f);
        if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().isTrigger = false;
        else GetComponent<SphereCollider>().isTrigger = false;
    }
	void Update() {
        Destroy(gameObject, 20f);
        if (destroy)
        {
            switch (kuromame)
            {
                case false:
                    size -= size/30;
                    transform.localScale = new Vector3(size,size,size);
                    Destroy(gameObject,0.3f);
                break;
                case true:
                    size -= size/80;
                    transform.localScale = new Vector3(size,size,size);
                    Destroy(gameObject,10f);
                break;
            }
        }
	}
    void OnCollisionEnter(Collision col) {
        //void OnTriggerEnter (Collider col) {
        Debug.Log(col.collider.tag);
        if (!kinako&&col.collider.tag == "Enemy")
        {
            if (bulletcode == 8) Shot.edamamezisseki=1;
            if (miso)
            {
                MISObust();
            }
            else
            {
                if (bulletcode == 3) {
                    hit++;
                    if (hit >= 3)
                    {
                        Social.ReportProgress("CgkIz5_nxIYREAIQCA", 100.0f, (bool success) => {
                            // handle success or failure
                        });
                    }
                }
                col.gameObject.SendMessage("Damage", attack);
                col.gameObject.SendMessage("bulletcode", bulletcode);
                if (!Throug) Destroy(gameObject);
            }
		}
		if (col.collider.tag == "Untagged") {
            if (kinako)
            {
                Invoke("MISObust",0.7f);
            }else if(miso){
                MISObust();
            }
            else destroy = true;
        }
        if(col.collider.tag == "Shild")
        {
            Destroy(gameObject);
        }
	}
    void MISObust()
    {
        GameObject obj = GameObject.Instantiate(misobust);
        obj.transform.position = transform.position;
        obj.GetComponent<Misobust>().attack = attack;
        obj.GetComponent<Misobust>().bulletcode = bulletcode;
        Destroy(gameObject);
    }
    public shot ShotSet
    {
        set
        {
            Shot = value;
        }
    }
}