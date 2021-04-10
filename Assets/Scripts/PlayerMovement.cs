using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D _rb;
    Animator _am;
    AudioSource _as;
    public float velocity = 50f;
    private float rotationSpeed = 80;
    bool canRotate = true;
    bool canTakeDamage = true;

    public AudioClip[] clips;

    EventSystem m_EventSystem;
    GraphicRaycaster m_Raycaster;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _am = GetComponent<Animator>();
        _as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Scree.height

        if ( Input.GetKeyDown(KeyCode.Space) && canRotate && Time.timeScale == 1)
        {
            _rb.velocity = Vector2.up * velocity;
            if (!_as.isPlaying) {
                _as.clip = clips[Random.Range(0, 11)];
                _as.Play();
            }

        }
        else if (Input.GetMouseButtonDown(0) && canRotate && Time.timeScale == 1)
        {
            Vector3 mousePos = Input.mousePosition;
            if (mousePos.x < (Screen.width * 0.91f) || mousePos.x > (Screen.width * 0.965f) || mousePos.y < (Screen.height * 0.85f) || mousePos.y > (Screen.height * 0.95f))
            {

                _rb.velocity = Vector2.up * velocity;
                if (!_as.isPlaying)
                {
                    _as.clip = clips[Random.Range(0, 11)];
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
                if (mousePos.x < (Screen.width * 0.91f) || mousePos.x > (Screen.width * 0.965f) || mousePos.y < (Screen.height * 0.85f) || mousePos.y > (Screen.height * 0.95f))
                {

                    _rb.velocity = Vector2.up * velocity;
                    if (!_as.isPlaying)
                    {
                        _as.clip = clips[Random.Range(0, 11)];
                        _as.Play();
                    }
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {

            StartCoroutine("Invulnerability");

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
    }

    IEnumerator Invulnerability()
    {
        canRotate = false;
        _rb.gravityScale = 0;
        canTakeDamage = false;
        _am.Play("PC_Fall");
        _rb.velocity = Vector2.zero;

        StartCoroutine("Fade");

        yield return new WaitForSeconds(2);
        
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        GetComponent<SpriteRenderer>().color = tmp;
        canRotate = true;
        _rb.gravityScale = 1;
        canTakeDamage = true;

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
