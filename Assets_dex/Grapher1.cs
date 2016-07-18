using UnityEngine;
using System.Collections;

public class Grapher1 : MonoBehaviour {
    [Range(10, 100)]
    public int resolution = 100;
    private int currentResolution;
    private ParticleSystem.Particle[] points;
    // Use this for initialization

    void Start()
    {
        CreatePoints();
    }

    private void CreatePoints () {

        if (resolution < 10 || resolution > 100)
        {
            Debug.LogWarning("Grapher resolution out of bounds, resetting to minimum.");
            resolution = 10;
        }
        currentResolution = resolution;
        points = new ParticleSystem.Particle[resolution];
        float increment = 1f / (resolution - 1);
        for (int i = 0; i < resolution; i++)
        {
            float x = i * increment;
            //float y = i * increment;
            //float z = i * increment;
            //Debug.Log(x);
            //Debug.Log(y);
            points[i].position = new Vector3(x, 0f, 0f);
            points[i].color = new Color(x, 0f, 0f);
            points[i].size = 0.1f;
        };
    }
	
	// Update is called once per frame
	void Update () {
        if (currentResolution != resolution || points == null)
        {
            CreatePoints();
        }
        for (int i = 0; i < resolution; i++)
        {
            Vector3 p = points[i].position;
            p.y= Sine(p.x);
            points[i].position = p;
            Color c = points[i].color;
            c.g = p.y;
            points[i].color = c;
        }
        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
	}
    private static float Sine(float x)
    {
        return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x);
    }
}
