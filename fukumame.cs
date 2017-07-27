using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fukumame : MonoBehaviour {
    [SerializeField]
    private GameObject[] bullet = new GameObject[5];
    [SerializeField]
    private Transform shotpoint;
    private bool push = false;
    private float time = 0;
	[SerializeField]
    private int power = 100;
    [SerializeField]
    private int bulletcode = 0;
    private int[] mainus = new int[2];
    private AudioSource se;
    public float gatobusttime = 5;
    private string[] soyname = new string[]{
        "油あげ",
        "爆発",
        "ガ豆",
        "遺伝子",
        "収穫祭"
    };
	[SerializeField]
    private Score score;
	private Image gageimage;
    [SerializeField]
    private AudioSource[] serihu = new AudioSource[5];
    // Use this for initialization
    void Start () {
		gageimage=GetComponent<Image> ();
		se = GetComponent<AudioSource>();
        bulletcode = PlayerPrefs.GetInt("hukumamecode", 0);
		transform.GetComponentInChildren<Text>().text = soyname[bulletcode]+"\n発動";
    }
    private float time2;
	// Update is called once per frame
	void Update () {
        time2 += Time.deltaTime;
        if (time2 >= 3)
        {
            time2 = 0;
            gageup=1;
        }
    }
    void aburaage()
    {
        GameObject obj = GameObject.Instantiate(bullet[bulletcode]) as GameObject;
        obj.transform.position = shotpoint.position;
    }
    void syseibun()
    {
        GameObject obj = GameObject.Instantiate(bullet[bulletcode]) as GameObject;
        obj.transform.position = shotpoint.position;
    }
    IEnumerator gato()
    {
        for(int i = 0; i < gatobusttime * 60; i++)
        {
            if (i % 5 == 0)
            {
                Handheld.Vibrate();//バイブレーションを起こす
                GameObject obj = GameObject.Instantiate(bullet[bulletcode]) as GameObject;
                obj.transform.position = shotpoint.position;
                obj.GetComponent<Rigidbody>().AddForce(shotpoint.forward * 2000);
            }
            yield return null;
        }
    }
	public int gageup{
		set{
			power += value;
			if (power > 100) power = 100;
			gageimage.fillAmount = (float)power / 100;
		}	
	}
    IEnumerator idensi()
    {
        score.kumikae = 2;
		power = 100;
		for (int i = 0; i < 500; i++) {
			if(i%5==0)power--;
			gageimage.fillAmount = (float)power / 100;
			yield return null;
		}
        score.kumikae = 1;
    }
	IEnumerator taretto()
    {
		Time.timeScale = 0.5f;
		yield return new WaitForSeconds(5);
		Time.timeScale = 1f;
    }
	public void buttonpush()
	{
        if (power >= 100)
        {
            power = 0;
            serihu[bulletcode].Play();
            switch (bulletcode)
            {
                case 0:
                    aburaage();
                    break;
                case 1:
                    syseibun();
                    break;
                case 2:
                    StartCoroutine("gato");
                    break;
                case 3:
                    StartCoroutine("idensi");
                    break;
                case 4:
					StartCoroutine("taretto");
                    break;
            }
        }
	}
}
