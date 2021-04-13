using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarMenuCanvasController : MonoBehaviour
{

    private AudioSource _as;
    // Start is called before the first frame update
    void Start()
    {
        /*
        PlayerPrefs.SetInt("PilarVisited", 0);
        PlayerPrefs.SetInt("MercadoVisited", 0);
        PlayerPrefs.SetInt("PlataVisited",  0);
        PlayerPrefs.SetInt("TeatroVisited", 0);
        PlayerPrefs.SetInt("AljaferiaVisited", 0);
        PlayerPrefs.SetInt("SeoVisited", 0);*/

        _as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        SceneManager.LoadScene("SergioScene");

    }

    public void credits()
    {
        SceneManager.LoadScene("TrophyScene");
    }

    public void exit()
    {
        Application.Quit();
    }

    public void options()
    {
        //SceneManager.LoadScene("Options");
    }

    public void playSound()
    {

        _as.Play();

    }


    }
