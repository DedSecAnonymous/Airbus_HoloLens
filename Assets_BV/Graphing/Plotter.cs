using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public class Plotter : MonoBehaviour
{
    private WWW get;
    public static string getreq;
    private float[] x1 = { 0.0f };
    private float[] y1 = { 0.0f };
    private float[] x2 = { 0.0f };
    private float[] y2 = { 0.0f };
    public static int counter = 0;
    public int resolution = 10;
    private int currentResolution;
    private ParticleSystem.Particle[] points;
    List<ParticleSystem.Particle> pt = new List<ParticleSystem.Particle>();
    int i = 0;
    // Use this for initialization
    void Start ()
    {
        //StartCoroutine(WaitForRequest());
        counter = 0;

    }
	
	// Update is called once per frame
	void Update ()
    {
        
        WFR2();
        StartCoroutine(Waiting());
        Debug.Log("pt.ToArray(): "+pt.ToArray()[pt.Count-1].position);
        GetComponent<ParticleSystem>().SetParticles(pt.ToArray(), pt.Count);

    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1);
    }

    private void CreatePoints(float[] a, float[] b)
    {
        currentResolution = resolution;
        points = new ParticleSystem.Particle[a.Length];
        resolution = a.Length;
        Debug.Log("Data in 'a': "+a[0]);
        Debug.Log("Length of 'a': "+resolution);
        for (int i = 0; i < resolution; i++)
        {            
            points[i].position = new Vector3(a[i], b[i], 0f);
            points[i].color = new Color(255, 0f, 0f);
            points[i].size = 0.1f;
        }
    }

    private IEnumerator WaitForRequest()
    {
        float num = UnityEngine.Random.Range(-10.0f, 10.0f);
        string url = "http://10.42.0.1:9000/api/comments?t=" + num.ToString();
        WWW get = new WWW(url);
        yield return get;
        getreq = get.text;
        if (get.error == null)
        {
            string json = @getreq;
            List<MyJSC> data = JsonConvert.DeserializeObject<List<MyJSC>>(json);
            int l = data.Count;
            Debug.Log("Getting data: "+data[l-1].content);
            Debug.Log("Split data: "+data[l-1].content.Split(',')[0]);
            //Debug.Log("Value of coord: "+x1[0]+","+y1[0]+","+x2[0]+","+y2[0]);
            string[] splitData = data[l - 1].content.Split(',');
            Debug.Log("splitData: "+splitData[0]+","+splitData[1] + "," + splitData[2] + "," + splitData[3]);
            Debug.Log("Counter: "+counter);
            x1[counter] = float.Parse(splitData[0]);
            y1[counter] = float.Parse(splitData[1]);
            x2[counter] = float.Parse(splitData[2]);
            y2[counter] = float.Parse(splitData[3]);
            Debug.Log("Revised value of coord: " + x1[counter] + "," + y1[counter] + "," + x2[counter] + "," + y2[counter]);
            counter++;
            Debug.Log("Latest Counter: "+counter);
            if (points==null)
            {
                Debug.Log("In points == null if loop");
                CreatePoints(x1, y1);
            }
            //for (int i = 0; i < counter; i++)
            //{
            //    Debug.Log("In for loop");
            //    Vector3 p = points[i].position;
            //    p.y = p.x;
            //    points[i].position = p;
            //    Color c = points[i].color;
            //    c.g = p.y;
            //    points[i].color = c;
            //}
            CreatePoints(x1, y1);
            GetComponent<ParticleSystem>().SetParticles(points, points.Length);
            Debug.Log("Points: "+points);
            Debug.Log("Points length: "+points.Length);
        }
        else
        {
            Debug.Log("Dayumn!-> " + get.error);
        }
    }

    private void WFR2()
    {
        float num = UnityEngine.Random.Range(-10.0f, 10.0f);
        string url = "http://10.42.0.1:9000/api/comments?t=" + num.ToString();
        WWW get = new WWW(url);
        while(!get.isDone)
        {
            Debug.Log("Waiting...");
        }
        getreq = get.text;
        if (get.error == null)
        {
            string json = @getreq;
            List<MyJSC> data = JsonConvert.DeserializeObject<List<MyJSC>>(json);
            int l = data.Count;
            Debug.Log("Getting data: " + data[l - 1].content);
            Debug.Log("Split data: " + data[l - 1].content.Split(',')[0]);
            //Debug.Log("Value of coord: "+x1[0]+","+y1[0]+","+x2[0]+","+y2[0]);
            string[] splitData = data[l - 1].content.Split(',');
            Debug.Log("splitData: " + splitData[0] + "," + splitData[1] + "," + splitData[2] + "," + splitData[3]);
            Debug.Log("Counter: " + counter);
            points = new ParticleSystem.Particle[1];
            points[0].position = new Vector3(0.2f*float.Parse(splitData[0])-10f, 5f*float.Parse(splitData[1]), 10.0f);
            points[0].color = new Color(255, 0, 0);
            points[0].size = 0.2f;
            pt.Add(points[0]);
            Debug.Log("Going back to update function and pt: "+pt);            
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
