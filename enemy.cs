using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {
    public float life = 1;
    GameObject player;
    bool stop = false;
    public float speed=1;
    float time;
    public AudioSource se;
    bool syouyu = false;
    public GameObject dedsound;
    public int upscore=1;
    public movemode move;
    public bool slimeogre=false;
    private int slimcount=0;
    private int Bulletcode;
    public enum movemode{
        nomal,
        gold
    }
    public bool boss=false;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Main Camera");
        if (move == movemode.gold)
        {
            transform.LookAt(player.transform);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x+UnityEngine.Random.Range(-60f, 60f), transform.eulerAngles.y, transform.eulerAngles.z);
            Destroy(gameObject, 20);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (syouyu)
        {
            time += Time.deltaTime;
            if (time >= 1)
            {
                time = 0;
                syouyu = false;
            }
        }
        switch (move)
        {
            case movemode.nomal:
                transform.LookAt(player.transform);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                if(!stop&&!syouyu)transform.position += transform.forward * 0.08f*speed;
            break;
            case movemode.gold:
                transform.position += transform.forward * 0.08f * speed;
            break;
        }
	}
    public void Damage(float damage)
    {
        se.Play();
        //被ダメージ処理 白点滅
        if (damage == 0)
        {
			time = 0;
            syouyu = true;
        }
        else
        {
            if (slimeogre&&slimcount>=50)
            {
                transform.localScale = new Vector3(transform.localScale.x - (transform.localScale.x / 45), transform.localScale.y - (transform.localScale.y / 45), transform.localScale.z - (transform.localScale.z / 45));
                speed += 0.08f;
                slimcount++;
            }
            GameObject effct=Instantiate(Resources.Load("hit") as GameObject);
            effct.transform.position = transform.position;
            Destroy(effct,1);
            if (syouyu) life -= damage*2; //体力から差し引く
            else life -= damage; //体力から差し引く
            if (life <= 0)
            {
                if (upscore == 0) upscore = GetComponent<Score>().scorepoint * 2;
                if (damage == 100) player.GetComponent<Score>().scoreupdate(upscore * 2);
                if (damage == 1000)player.GetComponent<Score>().scoreupdate(1);
                else player.GetComponent<Score>().scoreupdate(upscore);
                ded();
            }
        }
    }
    void bulletcode(int code)
    {
        Bulletcode = code;
    }
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player")
        {
            stop = true;
            player.GetComponent<jairon>().StartCoroutine("GameOver",transform);
        }
        if (col.gameObject.tag == "baria")
        {
            Destroy(col.gameObject);
            ded();
        }
    }
    void ded()
    {
        //体力が0以下になった時
        if (Bulletcode == 1)
        {
            if ((transform.position.x <= 1&&transform.position.x>=-1) || (transform.position.z <= 1&& transform.position.z >= -1))
            {
                Social.ReportProgress("CgkIz5_nxIYREAIQBg", 100.0f, (bool success) => {
                    // handle success or failure
                });
            }
        }
        if (Bulletcode == 6)
        {
            if ((transform.position.x >= 30 && transform.position.x <= -30) || (transform.position.z >= 30 && transform.position.z <= -30))
            {
                Social.ReportProgress("CgkIz5_nxIYREAIQBg", 100.0f, (bool success) => {
                    // handle success or failure
                });
            }
        }
        if (Bulletcode ==11)
        {
            
        }
        if (slimeogre&& PlayerPrefs.GetInt("soycode0", 0)==2&& PlayerPrefs.GetInt("soycode1", 0) == 2)
        {
            Social.ReportProgress("CgkIz5_nxIYREAIQBw", 100.0f, (bool success) => {
                // handle success or failure
            });
        }
        if(boss&& PlayerPrefs.GetInt("soycode0", 0) == 9 && PlayerPrefs.GetInt("soycode1", 0) == 9)
        {
            Social.ReportProgress("CgkIz5_nxIYREAIQDg", 100.0f, (bool success) => {
                // handle success or failure
            });
        }
        if (boss) GameObject.Find("StagePlane").GetComponent<enemyspawn>().bossded();
        GameObject obj = GameObject.Instantiate(dedsound);
        obj.transform.position = transform.position;
        Destroy(this.gameObject);   //自身を削除
    }
}
