using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
namespace SocialConnector
{
    public class equipmenu : MonoBehaviour
    {
        [SerializeField] private
        AudioSource[] se = new AudioSource[2];
        int changearm;
        [SerializeField]
        private GameObject equipmenuobj;
        [SerializeField]
        private GameObject hukumameequipmenuobj;
        [SerializeField]
        private GameObject okarisitaobj;
        private int weponcode;
        private string codename;
        [SerializeField]
        private GameObject[] chose = new GameObject[3];
        [SerializeField]
        private Transform[] tipsobj = new Transform[4];
        [SerializeField]
        private GameObject[] tutolial = new GameObject[4];
        private int int_tutolial = 0;
        [SerializeField]
        private Sprite[] soyimage;
        [SerializeField]
        private Sprite[] hukumameimage;
        [SerializeField]
        private Image fede;
        [SerializeField]
        private string[,] tips = new string[,]{
            {"大豆","平均的な連豆速度と豆速そして大豆力を持つ。\n遺伝子組み換えでない正真正銘の大豆。"},
            {"納豆","散弾となり近距離に有効だが連豆性能と豆速が劣る。\n発酵したことにより大豆力と重みを増した。"},
            {"豆乳","近距離に適した高い連豆速度持つ。\n大豆力は下がったがそれを高い連豆速度で補っている小鬼向け。"},
            {"もやし","遠距離向けで豆速が非常に高い。反面連豆性能に劣る。\n細長い形状は鬼の狙撃に非常に適している。"},
            {"味噌","発射して一定時間後に爆発する。\n密集した鬼に大して高い大豆力を発揮する。"},
            {"醤油","大豆力は低いが、当たった鬼を弱体化する能力を持つ。\n全ての大豆とすばらしい相性を見せる大豆の名サポーター"},
            {"豆腐","重く命中率が低いが、極めて高い大豆力を持つ。\n四角く白くそして脆すぎた。それはまさに大豆の塊であった。"},
            {"ずんだ","長押しでチャージをして放たれる大豆力は鬼さえ貫く大豆力を持つ。\n今一番HOTな大豆。甘味から塩味までさまざまな応用が利く。"},
            {"枝豆","水平に3発の大豆を放つ。癖の無く当てやすいのが特徴。\nあまり知られてないが立派な大豆である。大人達の心強い味方。"},
            {"ソイバー","至近距離にしか効果が無いがソイバーで手に入るスコアは2倍になる。\n海を超えてやってきた若きサムライ。"},
            {"黒豆","重く豆速低いが地面に落ちても直ぐには消えず転がる。\n黒いから性格が悪い印象を受けるがとんでもない。ホントは良いヤツ。"},
            {"きな粉","命中と同時に爆発し継続した広いダメージ領域を発生させる。\n挽いたそれは瞬間的な大豆力に劣るが範囲に入った鬼を容赦なく蝕む。"}
        };
        [SerializeField]
        private string[,] hukutips = new string[,]{
            {"油揚げ","一回触れられてもゲームオーバーにならない。\n大豆の鎧を纏う事で鬼に対抗する。使ったあとは手を洗おう。"},
            {"主成分爆発","大豆の力を爆発させ全ての鬼を一掃する。得られるスコアは少ない。\n主成分であるタンパク質のエネルギーは全ての鬼をたやすく葬る。"},
            {"ガ豆リング","高精度・高連豆性能・高大豆力の大豆を掃射する。\nスーパーでは売ってない高級品を贅沢に使用した一品。"},
            {"遺伝子組み換え","本来禁忌とされる遺伝子の組み換えを行い、得られるスコアを2倍にする。\n長時間の使用は消費者の信頼に影響する。"},
			{"収穫祭","収穫の祈りを天に届かせることで一時的に時の流れを遅くする。\n時を操ることで短時間で大量の収穫を可能とする信仰魔法。"}
        };
        [SerializeField]
        private GameObject mainmenu;
        [SerializeField]
        private GameObject CameraSetting;
        [SerializeField]
        private GameObject runkwindow;
        // Use this for initialization
        void Start()
        {
            GameObject equiptips = GameObject.Find("equiptips");
            codename = "soycode" + changearm;
            tipsset(PlayerPrefs.GetInt(codename, 0));
            hukumametipsset(PlayerPrefs.GetInt("hukumamecode", 0));
            chose[0].transform.FindChild("Image/SoyImage").GetComponent<Image>().sprite = soyimage[PlayerPrefs.GetInt("soycode0", 0)];
            chose[1].transform.FindChild("Image/SoyImage").GetComponent<Image>().sprite = soyimage[PlayerPrefs.GetInt("soycode1", 0)];
            chose[2].transform.FindChild("Image/SoyImage").GetComponent<Image>().sprite = hukumameimage[PlayerPrefs.GetInt("hukumamecode", 0)];
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void changesoy(int arm)
        {
            if (arm != 2)
            {
                changearm = arm;
                codename = "soycode" + changearm;
                equipmenuobj.SetActive(true);
            }
            else
            {
                codename = "hukumamecode";
                hukumameequipmenuobj.SetActive(true);
            }
        }
        public void tipsset(int soycode)
        {
            tipsobj[0].GetComponent<Text>().text = tips[soycode, 0];
            tipsobj[1].GetComponent<Text>().text = tips[soycode, 1];
            weponcode = soycode;
        }
        public void hukumametipsset(int soycode)
        {
            changearm = 2;
            tipsobj[2].GetComponent<Text>().text = hukutips[soycode, 0];
            tipsobj[3].GetComponent<Text>().text = hukutips[soycode, 1];
            weponcode = soycode;
        }
        public void chosesoy()
        {
            se[0].Play();
            PlayerPrefs.SetInt(codename, weponcode);
            switch(changearm)
            {
                case 0:
                case 1:
                    chose[changearm].transform.FindChild("Image/SoyImage").GetComponent<Image>().sprite = soyimage[weponcode];
                    break;
                case 2:
                    chose[changearm].transform.FindChild("Image/SoyImage").GetComponent<Image>().sprite = hukumameimage[weponcode];
                    break;
            }
            close();
        }
        public void close()
        {
            equipmenuobj.SetActive(false);
            hukumameequipmenuobj.SetActive(false);
        }
        public void gamestart()
        {
            StartCoroutine(st());
        }
        public IEnumerator st()
        {
            fede.enabled = true;
            se[1].Play();
            AsyncOperation async = Application.LoadLevelAsync("Stage");//シーンのロード
            async.allowSceneActivation = false;//だがまだシーンの移動はしない
            float b = 0;
            while (b <= 1)
            {
                b += 0.01f;
                fede.color = new Color(1, 1, 1, 0 + b);
                yield return null;
            }

            Social.ReportProgress("CgkIz5_nxIYREAIQBQ",100.0f, (bool success) => {
                // handle success or failure
            });
            yield return new WaitForSeconds(2.5f);
            async.allowSceneActivation = true;//だがまだシーンの移動はしない
        }
        public void OnPushShare()
        {
            StartCoroutine(Share());
        }
        // シェア処理
        private IEnumerator Share()
        {
            // 画面をキャプチャ
            Application.CaptureScreenshot("screenShot.png");

            // キャプチャを保存するので１フレーム待つ
            yield return new WaitForEndOfFrame();

            // シェアテキスト設定
            string text = "アグレッシヴ節分ゲーム【GET!OUT!!OGRE!!!】\n今の最高点は" + PlayerPrefs.GetInt("highscore", 0) + "点\n #GetOutOgre ";
            string url = "https://shadowmunderbar.jimdo.com/";

            // キャプチャの保存先を指定
            string texture_url = Application.persistentDataPath + "/screenShot.png";

            // iOS側の処理を呼び出す
            SocialConnector.Share(text, url, texture_url);
        }
        
        public void credit(bool openmode)
        {
            okarisitaobj.SetActive(openmode);
        }

        public void camerasetting(bool openmode)
        {
            mainmenu.SetActive(!openmode);
            CameraSetting.SetActive(openmode);
            GameObject.Find("Main Camera").GetComponent<jairon>().enabled = openmode;
        }
        public void runkopen(bool openmode)
        {
            mainmenu.SetActive(!openmode);
            runkwindow.SetActive(openmode);
            if (openmode)
            {
                for (int i = 1; i < 10; i++)
                {
                    runkwindow.transform.FindChild(i.ToString()).GetComponent<Text>().text = i + "位" + PlayerPrefs.GetString("runkname"+ (i - 1), "") + "\n" + PlayerPrefs.GetInt("runkscore" + (i - 1), 0).ToString();
                }
            }
        }
        public void LeaderBoardOpen()
        {
            Social.ShowLeaderboardUI();
        }
        
        public void tutolial_event()
        {
            for (int i = 0; i < tutolial.Length; i++)
            {
                tutolial[i].SetActive(false);
            }
            switch (int_tutolial)
            {
                case 0:
                    int_tutolial++;
                    tutolial[0].SetActive(true);
                    tutolial[1].SetActive(true);
                    break;
                case 1:
                    int_tutolial++;
                    tutolial[0].SetActive(true);
                    tutolial[2].SetActive(true);
                    break;
                case 2:
                    int_tutolial++;
                    tutolial[0].SetActive(true);
                    tutolial[3].SetActive(true);
                    break;
                default:
                    int_tutolial = 0;
                    break;
            }
        }
        public void ShowAchievement()
        {
            Social.ShowAchievementsUI();
        }

    }
}
