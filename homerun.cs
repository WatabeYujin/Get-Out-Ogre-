using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homerun : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Damage(float damage)
    {
    }
    void bulletcode(int code)
    {
        if (code == 10)
        {
            Social.ReportProgress("CgkIz5_nxIYREAIQDw", 100.0f, (bool success) => {
                // handle success or failure
            });
        }
    }
}
