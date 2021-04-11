using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenuController : MonoBehaviour
{

    public GameObject pauseButton;

    public GameObject resumeButton;

    public GameObject exitButton;

    public GameObject blackCurtain;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            puaseGame();
        }
    }


    public void puaseGame()
    {
        if (Time.timeScale == 0)
        {

            pauseButton.SetActive(true);
            Time.timeScale = 1;
            resumeButton.SetActive(false);
            exitButton.SetActive(false);
            blackCurtain.SetActive(false);

        }
        else
        {

            pauseButton.SetActive(false);
            Time.timeScale = 0;
            resumeButton.SetActive(true);
            exitButton.SetActive(true);
            blackCurtain.SetActive(true);

        }
    }

    public void exitPlay()
    {
        SceneManager.LoadScene("StartScene");
    }

}

