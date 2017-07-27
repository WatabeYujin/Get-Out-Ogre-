using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
    public bool titlemode = false;
    public Text score;
    public int scorepoint;
    [HideInInspector]
    public int kumikae = 1;
    int highscore;
	[SerializeField]
	private fukumame Fuku;
	// Use this for initialization
	void Start () {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        if(!titlemode)score.text = "スコア　" + scorepoint + "点\n" + "最高スコア　" + highscore+"点";
        else score.text = "最高スコア" + PlayerPrefs.GetInt("highscore", 0);
	}
	
	// Update is called once per frame
    public void scoreupdate(int point)
    {
		Fuku.gageup= point*UnityEngine.Random.Range(1, 5);
        scorepoint += point*kumikae;
        if (highscore < scorepoint) highscore = scorepoint;
        score.text = score.text = "スコア　" + scorepoint + "点\n" + "最高スコア　" + highscore + "点";
    }
}
