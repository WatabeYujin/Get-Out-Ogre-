using UnityEngine;
using System.Collections;

public class enemyspawn : MonoBehaviour {
    public GameObject[] Enemy = new GameObject[3];
    public float spawntime = 3;
    float[] time=new float[3];
    float[] rd = new float[3];
    [SerializeField]
    private GameObject[] boss = new GameObject[3];
    int mainus = 0,p = 0,sumon=1,sumon_=1;
    private bool isboss=false;
	// Use this for initialization
    void Start()
    {
        StartCoroutine("enemyspawner");
    }
    IEnumerator enemyspawner()
    {
        while (true )
        {
            yield return new WaitForSeconds(spawntime + UnityEngine.Random.Range((spawntime + sumon) * 1f, (spawntime+sumon) / 1.2f));
            p++;
            if (p >= 3)
            {
                p = 0;
                sumon++;
            }
            if (sumon % 5 == 0)
            {
                sumon_ = sumon/3;
                if (!isboss)
                {
                    isboss = true;
                    GetComponent<AudioSource>().Stop();
                    if (UnityEngine.Random.Range(0, 2) == 0) rd[0] *= -1;
                    if (rd[0] <= 50)
                    {
                        rd[1] = UnityEngine.Random.Range(50f, 70f);
                        if (UnityEngine.Random.Range(0, 2) == 0) rd[1] *= -1;
                    }
                    else
                    {
                        rd[1] = UnityEngine.Random.Range(0f, 70f);
                        if (UnityEngine.Random.Range(0, 2) == 0) rd[1] *= -1;
                    }
                    GameObject obj = GameObject.Instantiate(boss[UnityEngine.Random.Range(0, boss.Length)]) as GameObject;
                    obj.transform.position = new Vector3(rd[0], 0.5f, rd[1]);
                    obj.GetComponent<enemy>().boss = true;
                }
            }else
            {
                sumon_ = sumon;
                rd[0] = UnityEngine.Random.Range(0f, 70f);
            }
            for (int i = 0; i < UnityEngine.Random.Range((sumon_+1)/2, sumon_+1); i++)
            {
                rd[0] = UnityEngine.Random.Range(0f, 70f);
                if (UnityEngine.Random.Range(0, 2) == 0) rd[0] *= -1;
                if (rd[0] <= 30) {
                    rd[1] = UnityEngine.Random.Range(30f, 70f);
                    if (UnityEngine.Random.Range(0, 2) == 0) rd[1] *= -1;
                }
                else
                {
                    rd[1] = UnityEngine.Random.Range(0f, 70f);
                    if (UnityEngine.Random.Range(0, 2) == 0) rd[1] *= -1;
                }
                GameObject obj = GameObject.Instantiate(Enemy[UnityEngine.Random.Range(0, Enemy.Length)]) as GameObject;
                obj.transform.position = new Vector3(rd[0], 0.5f, rd[1]);
            }
            if (sumon % 3 == 0&&p==0)
            {
                rd[0] = UnityEngine.Random.Range(0f, 70f);
                if (UnityEngine.Random.Range(0, 2) == 0) rd[0] *= -1;
                if (rd[0] <= 30) {
                    rd[1] = UnityEngine.Random.Range(30f, 70f);
                    if (UnityEngine.Random.Range(0, 2) == 0) rd[1] *= -1;
                }
                else
                {
                    rd[1] = UnityEngine.Random.Range(0f, 70f);
                    if (UnityEngine.Random.Range(0, 2) == 0) rd[1] *= -1;
                }
                GameObject obj = GameObject.Instantiate(Enemy[4]) as GameObject;
                obj.transform.position = new Vector3(rd[0], 0.5f, rd[1]);
            }
        }
    }
	// Update is called once per frame
	void Update () {
        /*
        time[0] += Time.deltaTime;
        if (time[0] >= spawntime[0])
        {
            time[0] = 0;
            for (int n = 0; n < 2; )
            {
                rd[n] = UnityEngine.Random.Range(10, 15);
            }
            GameObject obj = GameObject.Instantiate(Enemy[1]);
            obj.transform.position = new Vector3(rd[0], 0, rd[1]);
        }*/
	}
    public void bossded()
    {
        sumon++;
        GetComponent<AudioSource>().Play();
        isboss = false;
    }
}
