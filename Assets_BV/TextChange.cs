using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TextChange : MonoBehaviour {

    // Use this for initialization
    WWW get;
    public static string getreq;
    Text text;

    void Start ()
    {
        //string url = "http://192.168.137.31:9000/api/comments";
        //get = new WWW(url);
        StartCoroutine(WaitForRequest());
        text = GetComponent <Text> ();
	}

    // Update is called once per frame
    void Update()
    {
        /*string url = "http://192.168.137.31:9000/api/comments";
        get = new WWW(url);*/
        StartCoroutine(WaitForRequest());
        //text.text = "Data: " + get.text;

    }
    private IEnumerator WaitForRequest()
    {
        string url = "http://httpbin.org/get";
        WWW get = new WWW(url);
        yield return get;
        getreq = get.text;
        //check for errors
        if(get.error == null)
        {
            //Debug.Log("Ohhh Yeahhh!-> " + get.text);
            //Debug.Log(getreq.ToString());
            text.text ="Data: "+ getreq.ToString().Substring(0,20);
        }
        else
        {
            Debug.Log("Dayumn!-> "+get.error);
        }
    }

    
    
}
