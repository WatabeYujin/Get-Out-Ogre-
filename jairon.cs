using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.Advertisements;
public class jairon : MonoBehaviour
{
    /// <summary>
    /// スマホの傾きをカメラの向きに反映させる
    /// </summary>
    private int totalsoypoint;
    private GUIStyle style;
    public Quaternion gyro;
    Quaternion gyrodefo;
    Vector3 gyrosan, ja;
    public float regyrox;
    float gyrosanX, gyrosanY, fgyrox, fgyrox2;
    public bool gameover = false;
    public Transform redercamera;
    public GameObject[] deleteobj=new GameObject[5];
    public AudioSource ded;
	[SerializeField]
	private fukumame Fuku;
    [SerializeField]
    private GameObject playerobj;
    [SerializeField]
    enemyspawn spawn;
    // Use this for initialization
    void Awake()
    {
        Advertisement.Initialize("1474966", false);
    }
    void Start()
    {
        if(PlayerPrefs.GetInt("soycode0", 5) == 2 && PlayerPrefs.GetInt("soycode1", 0) == 5)
        {
            Invoke("syouyu", 60);
        }
		regyrox=PlayerPrefs.GetFloat("Setgyro", 0);
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        redercamera.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);
        if (Input.gyro.enabled)
        {
            gyro = Input.gyro.attitude;
            //gyro = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
            //gyro = Quaternion.Euler(0, 0, 0);
            gyro = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
            gyrosan = gyro.eulerAngles;
            gyrosan.y += fgyrox - fgyrox2;

            gyrosan.x-= regyrox;//ここで縦の傾きを調節する
            //カメラの稼動限界を調節する部分
            gyrosanX = (gyrosan.x > 180) ? gyrosan.x - 360 : gyrosan.x;
            gyrosanY = (gyrosan.y > 180) ? gyrosan.y - 360 : gyrosan.y;
            //変更点
            gyrosan.y = (gyrosanY < 0) ? gyrosanY + 360 : gyrosanY;
            gyrosan.x = (gyrosanX < 0) ? gyrosanX + 360 : gyrosanX;
            //変更点ここまで
            //スマホの向きにカメラを傾ける
            gameObject.transform.rotation = Quaternion.Euler(gyrosan);
        }
    }
    public IEnumerator GameOver(Transform enemy)
    {
        if (!gameover)
        {
            spawn.enabled = false;
            Time.timeScale = 1f;
            playerobj.transform.parent = null;
			Destroy (Fuku);
            ded.Play();
            gameover = true;
            Input.gyro.enabled = false;
            
            for(int i = 0; i < 2; i++)
            {
                totalsoypoint+=deleteobj[i].GetComponent<shot>().getsoypoint;
            }
            ((PlayGamesPlatform)Social.Active).IncrementAchievement(
            "CgkIz5_nxIYREAIQFg", totalsoypoint, (bool success) => { });
            for (int i = 0; i < deleteobj.Length; i++)
            {
                deleteobj[i].SetActive(false);
            }
            transform.LookAt(new Vector3(enemy.transform.position.x, enemy.transform.position.y - 0.7f, enemy.transform.position.z));
            for (int i = 0; i < 50;i++)
            {
                transform.position -= transform.forward * 0.2f;
                yield return null; 
            }
            int scorep = GetComponent<Score>().scorepoint;
            if(scorep>=500&& PlayerPrefs.GetInt("soycode0", 0) == 7 && PlayerPrefs.GetInt("soycode1", 0) == 7)
            {
                Social.ReportProgress("CgkIz5_nxIYREAIQDA", 100.0f, (bool success) => {
                    // handle success or failure
                });
            }
            if (scorep >= 500 && PlayerPrefs.GetInt("soycode0", 0) ==11 && PlayerPrefs.GetInt("soycode1", 0) == 11)
            {
                Social.ReportProgress("CgkIz5_nxIYREAIQEA", 100.0f, (bool success) => {
                    // handle success or failure
                });
            }
            if (PlayerPrefs.GetInt("highscore", 0) < scorep)
            {
                PlayerPrefs.SetInt("highscore", scorep);
                Debug.Log("ここでリーダーボード登録");
                
                Social.ReportScore(scorep, "CgkIz5_nxIYREAIQAQ", (bool success) =>
                {
                    if (success)
                    {
                        //登録成功時の処理
                    }
                    else
                    {
                        //登録失敗時の処理
                    }
                });
            }
            /*
            //学園祭用
            GetComponent<runking>().score_set = scorep;
            GetComponent<runking>().enabled = true;
            */
            yield return new WaitForSeconds(1.5f);
            if (Advertisement.IsReady()&& 0==Random.Range(0, 6)) Advertisement.Show();
            Application.LoadLevelAsync("title");//シーンのロード
            
        }
    }
	public void settinggyro(){
        gyro = Input.gyro.attitude;
        gyro = Quaternion.Euler(90, 0, 0) * new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w);
        PlayerPrefs.SetFloat("Setgyro", gyro.eulerAngles.x);
        regyrox = gyro.eulerAngles.x;
	}
	public void resetgyro(){
		PlayerPrefs.SetFloat("Setgyro", 0);
		regyrox = 0;
	}
    void syouyu()
    {
        Social.ReportProgress("CgkIz5_nxIYREAIQCg", 100.0f, (bool success) => {
            // handle success or failure
        });
    }
}

