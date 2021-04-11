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
