using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
public class onided : MonoBehaviour {
    [SerializeField]
    private string id;
    public AudioSource[] voice = new AudioSource[2];
	// Use this for initialization
	void Start () {
        if(id!=null)
        {
            Social.ReportProgress(id, 100.0f, (bool success) => {
                // handle success or failure
            });
            if (PlayerPrefs.GetInt(id, 0) == 0)
            {
                ((PlayGamesPlatform)Social.Active).IncrementAchievement(
                "CgkIz5_nxIYREAIQFQ", 1, (bool success) => { });
            }
            PlayerPrefs.SetInt(id, 1);
        }
        voice[UnityEngine.Random.Range(0, 2)].Play();
        Destroy(gameObject, 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
