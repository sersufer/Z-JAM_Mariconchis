using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public Camera cam;
    public Collider2D collider2D;
    [Header("UI")]
    public Text winText;
    public Text loseText;
    public Image space;
    public GameObject exit;

    public enum GamePhase
    {
        First,
        Final,
        MiniGame,
        Win,
        Lose
    }

    public GamePhase gamePhase;

    Rigidbody2D _rb;
    Animator _am;
    AudioSource _as;
    public float velocity = 50f;
    private float rotationSpeed = 80;
    bool canRotate = true;
    public bool isAlive = true;
    private int spaceCount = 0;
    private float gameTime;

    public AudioClip[] clips;

    EventSystem m_EventSystem;
    GraphicRaycaster m_Raycaster;


    // Start is called before the first frame update
    void Start()
    {
        gamePhase = GamePhase.First;
        _rb = GetComponent<Rigidbody2D>();
        _am = GetComponent<Animator>();
        _as = GetComponent<AudioSource>();
        gameTime = Time.time + 60;
        collider2D = gameObject.GetComponent<Collider2D>();


    }

    // Update is called once per frame
    void Update()
    {
        //Scree.height
        if (gamePhase == GamePhase.First) {
            if (Input.GetKeyDown(KeyCode.Space) && canRotate && Time.timeScale == 1)
            {
                _rb.velocity = Vector2.up * velocity;
                if (!_as.isPlaying) {
                    _as.clip = clips[Random.Range(0, 10)];
                    _as.Play();
                }

            }
            else if (Input.GetMouseButtonDown(0) && canRotate && Time.timeScale == 1)
            {
                Vector3 mousePos = Input.mousePosition;
                if (mousePos.x < (Screen.width * 0.9f) || mousePos.x > (Screen.width * 0.965f) || mousePos.y < (Screen.height * 0.81f) || mousePos.y > (Screen.height * 0.95f))
                {

                    _rb.velocity = Vector2.up * velocity;
                    if (!_as.isPlaying)
                    {
                        _as.clip = clips[Random.Range(0, 10)];
                        _as.Play();
                    }
                }


            }

            if (Input.touchCount > 0 && canRotate)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && Time.timeScale == 1)
                {
                    Vector3 mousePos = touch.position;
                    if (mousePos.x < (Screen.width * 0.9f) || mousePos.x > (Screen.width * 0.965f) || mousePos.y < (Screen.height * 0.81f) || mousePos.y > (Screen.height * 0.95f))
                    {

                        _rb.velocity = Vector2.up * velocity;
                        if (!_as.isPlaying)
                        {
                            _as.clip = clips[Random.Range(0, 10)];
                            _as.Play();
                        }
                    }

                }
            }

            if (Input.GetKeyDown(KeyCode.D))
            {

                StartCoroutine("Invulnerability");
                isAlive = false;

            }

            if (_rb.velocity.y > 0 && canRotate)
            {

                transform.eulerAngles = new Vector3(0, 0, 20);
                if (!_am.GetCurrentAnimatorStateInfo(0).IsName("PC_Flight"))
                {
                    _am.Play("PC_Flight");
                }

            }

            if (_rb.velocity.y <= 0 && canRotate)
            {

                transform.eulerAngles = new Vector3(0, 0, transform.rotation.eulerAngles.z - (Time.deltaTime * rotationSpeed));
                float nextRotation = transform.rotation.eulerAngles.z - (Time.deltaTime * rotationSpeed);
                if (nextRotation < 339 && nextRotation > 20)
                {
                    transform.eulerAngles = new Vector3(0, 0, 339);
                }
                if (!_am.GetCurrentAnimatorStateInfo(0).IsName("PC_Fall"))
                {
                    _am.Play("PC_Fall");
                }

            }
        }else
        {
            _rb.gravityScale = 0;
            _rb.velocity = Vector2.zero;

            transform.eulerAngles = new Vector3(0, 0, 0);
            if (cam.WorldToScreenPoint(transform.position).x < (Screen.width / 2))
            {
                transform.position += Vector3.right * 5 * Time.deltaTime;
                if (!_am.GetCurrentAnimatorStateInfo(0).IsName("PC_Flight") && gamePhase != GamePhase.Lose)
                {
                    _am.Play("PC_Flight");
                }
            }

            if(cam.WorldToScreenPoint(transform.position).x >= (Screen.width / 2) && gamePhase != GamePhase.Lose)
            {
                if (gamePhase == GamePhase.Final)
                {
                    if(spaceCount < 1)
                    space.enabled = true;
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        gamePhase = GamePhase.MiniGame;
                    }

                }
                
                if (gamePhase == GamePhase.MiniGame)
                {
                    if (!_am.GetCurrentAnimatorStateInfo(0).IsName("PC_Win"))
                    {
                        _am.Play("PC_PoopRedy");
                    }
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        spaceCount++;
                    }
                    if(spaceCount >= 15){
                        gamePhase = GamePhase.Win;
                    }

                }
                if (gamePhase == GamePhase.Win)
                {
                    space.enabled = false;
                    winText.enabled = true;
                    exit.SetActive(true);
                    _am.Play("PC_Win");
                }
            }

        }
        if (!isAlive)
        {
            gamePhase = GamePhase.Lose;
            _rb.gravityScale = 0;
            _rb.velocity = Vector2.zero;
            loseText.enabled = true;
            exit.SetActive(true);
            _am.Play("PC_PoopRedy");
        }
        if (Time.time >= gameTime && gamePhase == GamePhase.First)
        {
            gamePhase = GamePhase.Final;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isAlive = false;
        }
    }


    IEnumerator Invulnerability()
    {
        canRotate = false;
        _rb.gravityScale = 0;
        _am.Play("PC_Fall");
        _rb.velocity = Vector2.zero;

        StartCoroutine("Fade");

        yield return new WaitForSeconds(2);
        
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        GetComponent<SpriteRenderer>().color = tmp;
        canRotate = true;
        _rb.gravityScale = 1;

    }
    IEnumerator Fade()
    {
        for (float ft = 0; ft < 10; ft++)
        {
            if(ft%2 == 0) {
                Color tmp = GetComponent<SpriteRenderer>().color;
                tmp.a = 0f;
                GetComponent<SpriteRenderer>().color = tmp;
            } else
            {
                Color tmp = GetComponent<SpriteRenderer>().color;
                tmp.a = 1f;
                GetComponent<SpriteRenderer>().color = tmp;
            }
            yield return new WaitForSeconds(.2f);

        }
    }

}
