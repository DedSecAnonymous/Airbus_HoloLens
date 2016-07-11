using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GEText_Manager : MonoBehaviour {


    public static string getreq;
    Text text;
    // Use this for initialization
    void Start () {
        string url = "http://192.168.137.31:9000/api/comments";
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        text = GetComponent<Text>();


    }

    private IEnumerator WaitForRequest(WWW www)
    {
        //throw new NotImplementedException();
        yield return www;
        if (www.error == null)
        {
            Debug.Log("WWW OK : " + www.text);
            getreq = www.text;

        }else
        {
            Debug.Log("WWW Error" + www.error);
        }
    }


/*    void Awake()
    {
        text = GetComponent<Text>();
        score = 0;
    }
    */

    void Update()
    {
        Debug.Log(getreq.ToString());
        text.text = "DATA: " + getreq.ToString()[14];
    }
}
