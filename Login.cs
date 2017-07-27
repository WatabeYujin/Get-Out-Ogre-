using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class Login : MonoBehaviour {
    [SerializeField]
    bool Signbool;
    //Text text;
    
	// Use this for initialization
	void Start () {
        //text = transform.FindChild("Text").GetComponent<Text>();
#if UNITY_EDITOR
        Signbool = true;
#elif UNITY_ANDROID
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            Debug.Log("login: " + success);
            Signbool = success;
            if (success)
            {
                //サインイン成功
                
            }
            else
            {
                //サインイン失敗
            }
        });
#endif
        textchange();
    }
	// Update is called once per frame
	void Update () {
	}
    public void SignInOut()
    {
#if UNITY_EDITOR
        Signbool = !Signbool;
#elif UNITY_ANDROID
        if (Signbool)
        {
            Signbool = false;
            ((PlayGamesPlatform)Social.Active).SignOut();
        }
        else
        {
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) =>
            {
                Debug.Log("login: " + success);
                Signbool = success;
                if (success)
                {
                    //サインイン成功

                }
                else
                {
                    //サインイン失敗
                }
            });
        }
#endif
        textchange();
    }
    void textchange()
    {
        switch (Signbool)
        {
            case true:
                //text.text = "サインアウト";
                break;
            case false:
                //text.text = "サインイン";
                break;
            default:
                break;
        }
    }
}
