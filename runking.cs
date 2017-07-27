using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class runking : MonoBehaviour {
    private int Get_score;
    private int runk;
    [SerializeField]
    private GameObject runkinwindow;
    [SerializeField]
    private Text namespase;

    private string[] defname =
    {
      "大豆先生" ,"豆撒い太郎","節分野郎","特攻野郎豆チーム","名無しさん",
      "I'm Soy","富士っ子","おたふく納豆","ずんずん教","ずんだぁ",
      "ねばだ～","ずんだろっ","ホライずん","豆ｼｳﾞｧ先生","大豆のヤベー奴",
      "節分のヤベー奴","例の豆","ジャックと豆の木","一般通過鬼","オーガ",
      "節分って2月じゃね","畑の肉","遠藤","豆鉄砲","マメルスⅢ世"
    };
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void runkchack()
    {
        for(int i = 0; i < 9; i++)
        {
            if (PlayerPrefs.GetInt("runkscore" + i, 0) < Get_score)
            {
                runk = i;
                break;
            }
        }
        if (runk != -1) RunkIn();
        else StartCoroutine("bucktitle");
    }
    public int score_set
    {
        set
        {
            runk = -1;
            Get_score = value;
            runkchack();
        }
    }
    void RunkIn()
    {
        runkinwindow.SetActive(true);
    }
    IEnumerator bucktitle()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevelAsync("title");//シーンのロード
    }
    public void textaspect()
    {
        string name = namespase.text;
        if(name == "")
        {
            name =defname[Random.Range(0, defname.Length)];
        }
        for (int i = 7; i > runk; i--)
        {
            PlayerPrefs.SetString("runkname" + i, PlayerPrefs.GetString("runkname" + (i - 1), ""));
            PlayerPrefs.SetInt("runkscore" + i, PlayerPrefs.GetInt("runkscore" + (i - 1), 0));
        }
        PlayerPrefs.SetString("runkname" + runk, name);
        PlayerPrefs.SetInt("runkscore" + runk, Get_score);
        runkinwindow.SetActive(false);
        StartCoroutine("bucktitle");
    }
}
