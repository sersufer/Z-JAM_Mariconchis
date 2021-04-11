using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrophyMenuCanvasController : MonoBehaviour
{

    [Header("MonumentButtons")]
    #region MonumentButtons
    public GameObject elPlataButton;
    public GameObject laSeoButton;
    public GameObject mercadoCentralButton;
    public GameObject pilarButton;
    public GameObject teatroButton;
    public GameObject aljaferiaButton;
    #endregion

    [Header("Showcase")]
    #region Showcase
    public GameObject grayMonPanel;
    public GameObject elPlata;
    public GameObject laSeo;
    public GameObject mercadoCentral;
    public GameObject pilar;
    public GameObject teatro;
    public GameObject aljaferia;
    #endregion


    private AudioSource _as;

    // Start is called before the first frame update
    void Start()
    {

        _as = GetComponent<AudioSource>();
        grayMonPanel.SetActive(false);

        if (PlayerPrefs.GetInt("PlataVisited") == 1)
        {
            elPlataButton.GetComponent<Image>().color = Color.white;
            elPlataButton.GetComponentInChildren<Text>().enabled = false;
        } else
        {
            elPlataButton.GetComponent<Image>().color = Color.black;
        }

        if (PlayerPrefs.GetInt("SeoVisited") == 1)
        {
            laSeoButton.GetComponent<Image>().color = Color.white;
            laSeoButton.GetComponentInChildren<Text>().enabled = false;
        } else
        {
            laSeoButton.GetComponent<Image>().color = Color.black;
        }

        if (PlayerPrefs.GetInt("MercadoVisited") == 1)
        {
            mercadoCentralButton.GetComponent<Image>().color = Color.white;
            mercadoCentralButton.GetComponentInChildren<Text>().enabled = false;
        } else
        {
            mercadoCentralButton.GetComponent<Image>().color = Color.black;
        }

        if (PlayerPrefs.GetInt("PilarVisited") == 1)
        {
            pilarButton.GetComponent<Image>().color = Color.white;
            pilarButton.GetComponentInChildren<Text>().enabled = false;
        } else
        {
            pilarButton.GetComponent<Image>().color = Color.black;
        }

        if (PlayerPrefs.GetInt("TeatroVisited") == 1)
        {
            teatroButton.GetComponent<Image>().color = Color.white;
            teatroButton.GetComponentInChildren<Text>().enabled = false;
        } else
        {
            teatroButton.GetComponent<Image>().color = Color.black;
        }

        if (PlayerPrefs.GetInt("AljaferiaVisited") == 1)
        {
            aljaferiaButton.GetComponent<Image>().color = Color.white;
            aljaferiaButton.GetComponentInChildren<Text>().enabled = false;
        } else
        {
            aljaferiaButton.GetComponent<Image>().color = Color.black;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {

        SceneManager.LoadScene("StartScene");

    }

    public void enablePlata()
    {
        if (PlayerPrefs.GetInt("PlataVisited") == 1)
        {
            grayMonPanel.SetActive(true);
            elPlata.SetActive(true);
        }
    }

    public void disablePlata()
    {
        grayMonPanel.SetActive(false);
        elPlata.SetActive(false);
    }

    public void enableSeo()
    {
        if (PlayerPrefs.GetInt("SeoVisited") == 1)
        {
            grayMonPanel.SetActive(true);
            laSeo.SetActive(true);
        }
    }

    public void disableSeo()
    {
        grayMonPanel.SetActive(false);
        laSeo.SetActive(false);
    }

    public void enableMercadoCentral()
    {
        if (PlayerPrefs.GetInt("MercadoVisited") == 1)
        {
            grayMonPanel.SetActive(true);
            mercadoCentral.SetActive(true);
        }
    }

    public void disableMercadoCentral()
    {
        grayMonPanel.SetActive(false);
        mercadoCentral.SetActive(false);
    }

    public void enablePilar()
    {
        if (PlayerPrefs.GetInt("PilarVisited") == 1)
        {
            grayMonPanel.SetActive(true);
            pilar.SetActive(true);
        }
    }

    public void disablePilar()
    {
        grayMonPanel.SetActive(false);
        pilar.SetActive(false);
    }

    public void enableTeatro()
    {
        if (PlayerPrefs.GetInt("TeatroVisited") == 1)
        {
            grayMonPanel.SetActive(true);
            teatro.SetActive(true);
        }
    }

    public void disableTeatro()
    {
        grayMonPanel.SetActive(false);
        teatro.SetActive(false);
    }

    public void enableAljaferia()
    {
        if (PlayerPrefs.GetInt("AljaferiaVisited") == 1)
        {
            grayMonPanel.SetActive(true);
            aljaferia.SetActive(true);
        }
    }

    public void disableAljaferia()
    {
        grayMonPanel.SetActive(false);
        aljaferia.SetActive(false);
    }

    public void playSound()
    {
  
        _as.Play();

    }

}
