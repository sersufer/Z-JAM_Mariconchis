using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarMenuCanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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


}
