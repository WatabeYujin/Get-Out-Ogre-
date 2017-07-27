using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class shot : MonoBehaviour {
    int soypoint;
    [SerializeField]
    private GameObject[] bullet =new GameObject[8];
    [SerializeField]
    private Transform shotpoint;
    [SerializeField]
    private int arm = 1;
    private bool push = false;
    private float time = 0;
    [SerializeField]
    private int bulletcode=0;
    private int[] mainus=new int[2];
    private AudioSource se;
	[SerializeField]
	private GameObject gat;
    private float gatospeed;
    int edamame;
    private string[] soyname = new string[]{
        "大豆",
        "納豆",
        "豆乳",
        "もやし",
        "味噌",
        "醤油",
        "豆腐",
        "ずんだ",
        "枝豆",
        "ソイバー",
        "黒豆",
        "きな粉"
    };
    private float[,] BulletStatus = new float[,] {
    //{威力,弾速,連射}
        {2f,1f,0.35f},   //大豆
        {3f,0.9f,0.8f},   //納豆
        {1f,0.6f,0.2f},  //豆乳
        {5f,3f,1f},   //もやし
        {7f,0.7f,1.7f},      //味噌
        {1f,1.2f,0.6f},   //醤油
        {10f,0.6f,0.6f},   //豆腐
        {1f,1f,0.5f},    //ずんだ
        {3f,0.8f,0.7f},      //枝豆
        {100f,0f,0.7f},   //ソイバー
        {3f,0.3f,1f},    //黒豆
        {2f,0.8f,1.5f}   //きな粉
    };
    [SerializeField]
    private GameObject[] wepon = new GameObject[2];
    void Start()
    {
        se = GetComponent<AudioSource>();
        string codename = "soycode" + arm;
        bulletcode = PlayerPrefs.GetInt(codename,0);
        transform.GetComponentInChildren<Text>().text = soyname[bulletcode]+"\n発射";
        switch (bulletcode)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 5:
            case 8:
                wepon[0].SetActive(true);
                break;
            case 4:
            case 6:
            case 7:
            case 10:
            case 11:
                wepon[1].SetActive(true);
                break;
        }
    }
    public void PushDown(){
        if (bulletcode == 9&&time>=BulletStatus[9,2])
        {
            time = 0;
            GameObject soybar = GameObject.Instantiate(bullet[bulletcode]) as GameObject;
            soybar.transform.position = shotpoint.position;
            soybar.transform.parent = shotpoint;
            soybar.transform.eulerAngles = new Vector3(shotpoint.eulerAngles.x - 90, shotpoint.eulerAngles.y, shotpoint.eulerAngles.z);
        }
            push = true;
    }
 
    public void PushUp(){
            push = false;
    }
 
    void Update(){
        if (bulletcode <= 5 || bulletcode == 8)
        {
            switch (push)
            {
                case true:
                    gatospeed = Mathf.Clamp(gatospeed += 1f, 0, 30);
                    break;
                case false:
                    gatospeed = Mathf.Clamp(gatospeed -= 0.5f, 0, 30);
                    break;
            }
            gat.transform.eulerAngles += new Vector3(0, 0, gatospeed);
        }
        switch (bulletcode) { 
            case 7:
                if (push)
                {
                    time += Time.deltaTime;
                }
                if (!push && time != 0)
                {
                    Shot();
                    time = 0;
                }
                break;
            default:
                time += Time.deltaTime;
                if(push){
                    if (time > BulletStatus[bulletcode,2])
                    {
                        time = 0;
                        switch (bulletcode)
                        {
                            case 1:
                                for (int i = 0; i < 6; i++)
                                {
                                    if (i >= 3)
                                    {
                                        mainus[0] = -1;
                                    }
                                    else
                                    {
                                        mainus[0] = 1;
                                    }
                                    float[] rd=new float[2];
                                    for(int j = 0;j<2;j++){
                                        rd[j] = UnityEngine.Random.Range(1, 10);
                                        if (UnityEngine.Random.Range(0, 2) == 0) mainus[1] = -1;
                                        else mainus[1] = 1;
                                    }
                                    GameObject obj = GameObject.Instantiate(bullet[bulletcode]) as GameObject;
                                    obj.transform.position = shotpoint.position;
                                    obj.GetComponent<Rigidbody>().AddForce((
                                        shotpoint.forward + (mainus[0] * shotpoint.right) / rd[0])* (1000f * BulletStatus[bulletcode, 1]));
                                    obj.GetComponent<Rigidbody>().AddForce((
                                        shotpoint.forward + (mainus[1] * shotpoint.up) / rd[1]) * (1000f * BulletStatus[bulletcode, 1]));
                                }
                            break;
                            case 8:
                                    GameObject obj2 = GameObject.Instantiate(bullet[bulletcode]) as GameObject;
                                    if (bulletcode == 8) obj2.GetComponent<rifleBullet>().ShotSet = this;
                                    obj2.transform.position = shotpoint.position;
                                    obj2.GetComponent<rifleBullet>().attack = BulletStatus[bulletcode,0];
                                    obj2.GetComponent<Rigidbody>().AddForce((shotpoint.forward+(shotpoint.right / 8)) * BulletStatus[bulletcode, 1] * 2000);
                                    GameObject obj3 = GameObject.Instantiate(bullet[bulletcode]) as GameObject;
                                    if (bulletcode == 8) obj3.GetComponent<rifleBullet>().ShotSet = this;
                                    obj3.transform.position = shotpoint.position;
                                    obj3.GetComponent<rifleBullet>().attack = BulletStatus[bulletcode,0];
                                    obj3.GetComponent<Rigidbody>().AddForce((shotpoint.forward+(shotpoint.right / -8)) * BulletStatus[bulletcode, 1] * 2000);
                                break;
                        }
                        if(bulletcode!=9)Shot();
                    }
                }
            break;
        }
    }
 
    public void Shot(){
        if (bulletcode == 0)
        {
            soypoint++;
        }
        se.Play();
        if (bulletcode == 8)edamame = 0;
        GameObject obj = GameObject.Instantiate(bullet[bulletcode]) as GameObject;
        if (bulletcode == 8) obj.GetComponent<rifleBullet>().ShotSet = this;
        if (bulletcode!=7)obj.GetComponent<rifleBullet>().attack = BulletStatus[bulletcode,0];
        else
        {
            if (time > 0.5f)
            {
                if (time > 1)
                {
                    if (time > 2)
                    {
                        obj.GetComponent<rifleBullet>().Throug = true;
                        obj.GetComponent<rifleBullet>().attack = BulletStatus[bulletcode, 0] * 10;
                        obj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    }
                    else
                    {
                        obj.GetComponent<rifleBullet>().attack = BulletStatus[bulletcode, 0] * 5;
                        obj.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else
                {
                    obj.GetComponent<rifleBullet>().attack = BulletStatus[bulletcode, 0] * 3;
                    obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                }
            }
            else
            {
                obj.GetComponent<rifleBullet>().attack = BulletStatus[bulletcode, 0] * 1;
                obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }
        }
        obj.transform.position = shotpoint.position;
        obj.GetComponent<Rigidbody>().AddForce(shotpoint.forward * BulletStatus[bulletcode, 1] * 2000);
    }
    public int getsoypoint
    {
        get
        {
            return soypoint;
        }
    }
    public int edamamezisseki
    {
        set
        {
            edamame += value;
        }
    }
}