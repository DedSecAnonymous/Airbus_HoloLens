using UnityEngine;
using System.Collections;
using System;

public class Post_text : MonoBehaviour {
    public string posttext;

    // Use this for initialization
    void Start () {
        //setting up listener to get the text for post
        var input = gameObject.GetComponent<UnityEngine.UI.InputField>();
        var se = new UnityEngine.UI.InputField.SubmitEvent();
        se.AddListener(SubmitName);
        input.onEndEdit = se;

    }

    public void SubmitName(string arg0)
    {
            posttext = arg0;
            //Debug.Log(arg0);
            Debug.Log("incoming posttext");
            Debug.Log(posttext);

        //handling post request
        //handling the post req via post url
        string url = "http://httpbin.org/post";
        WWWForm form = new WWWForm();
        form.AddField("var1", posttext);
        //form.AddField("var2", "value2");
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));
    }

    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

         // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

   // Update is called once per frame
    void Update () {
	
	}
}
