using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Experimental.Video;
using UnityEngine.Video;

public class MoviePlayer : MonoBehaviour
{
    public Image screen;
    public VideoClip clip;
    //public MovieTexture movie;
    public VideoPlayer movie;

    private bool executing;

    private void Start()
    {
        this.gameObject.GetComponent<Renderer>().material.mainTexture = movie.texture;
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Confirm") || movie.isPlaying != true && executing != true)
        {
            StartCoroutine(FadeOut());
        }
    }
    private void OnDestroy()
    {
        if (GameManager.Instance.songSource.isPlaying)
        {
            GameManager.Instance.songSource.Stop();
        }
    }

    private IEnumerator FadeIn()
    {
        movie.Play();
        while (screen.color.a > 0f)
        {
            screen.color = new Color(screen.color.r, screen.color.g, screen.color.b, screen.color.a - 0.025f);
            yield return null;
        }
        yield return null;
    }

    private IEnumerator FadeOut()
    {
        executing = true;
        while (screen.color.a < 1f)
        {
            screen.color = new Color(screen.color.r, screen.color.g, screen.color.b, screen.color.a + 0.025f);
            yield return null;
        }
        switch (Application.loadedLevelName)
        {
            case "IntroCutscene":
                Application.LoadLevel("StartScreen");
                break;
        }
        executing = false;
        yield return null;
    }
}
