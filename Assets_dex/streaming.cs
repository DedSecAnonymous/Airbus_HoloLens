using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovieTextureStream : MonoBehaviour
{
    public Text progressGUI;
    public MeshRenderer targetRender = null;
    public AudioSource targetAudio = null;
    public string URLString = "http://unity3d.com/files/docs/sample.ogg";
    public WWW www;
    MovieTexture loadedTexture;

    void Start()
    {
        // Start download

        StartCoroutine(PlayMovie());
        www = new WWW(URLString);
        while (www.isDone == false)
        {
            //if (progressGUI != null) progressGUI.text = "Progresso do video: " + (int)(100.0f * www.progress) + "%";
           // yield return 0;
        }
    }

    IEnumerator PlayMovie()
    {

        if (targetRender == null) targetRender = GetComponent<MeshRenderer>();
        if (targetAudio == null) targetAudio = GetComponent<AudioSource>();

        www = new WWW(URLString);
        while (www.isDone == false)
        {
            //if (progressGUI != null) progressGUI.text = "Progresso do video: " + (int)(100.0f * www.progress) + "%";
            yield return 0;
        }

        loadedTexture = www.movie;
        while (loadedTexture.isReadyToPlay == false)
        {
            yield return 0;
        }
        targetRender.material.mainTexture = loadedTexture;
        targetAudio.clip = loadedTexture.audioClip;
        targetAudio.Play();
        loadedTexture.Play();
    }
}