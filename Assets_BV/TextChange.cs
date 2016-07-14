using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

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
        
        StartCoroutine(WaitForRequest());

    }
    private IEnumerator WaitForRequest()
    {

        string url = "http://10.42.0.1:9000/api/comments";
        WWW get = new WWW(url);
        yield return get;        
        getreq = get.text;        
        //getreq = File.ReadAllText(@"\Data\data.json");
        //yield return getreq;
        //check for errors
        if (get.error == null)
        {
            //Debug.Log("Ohhh Yeahhh!-> " + get.text);
            //Debug.Log(getreq.ToString());
            Debug.Log("To be parsed: " + getreq.ToString());
            string json = @getreq;
            List<MyJSC> data = JsonConvert.DeserializeObject<List<MyJSC>>(json);
            int l = data.Count;
            Debug.Log("Latest Data: " + data[l - 1].content);
            text.text = "Data: " + data[l - 1].content;
        }
        else
        {
            Debug.Log("Dayumn!-> " + get.error);
        }
        
    }  
    
}

public class MyJSC
{
    public string _id;
    public string author;
    public string content;
    public string _v;
    public string date;
}
