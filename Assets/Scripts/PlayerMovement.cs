using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D _rb;
    Animator _am;
    public float velocity = 50f;
    private float rotationSpeed = 80;
    bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _am = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            _rb.velocity = Vector2.up * velocity;

        }

        if(_rb.velocity.y >= 0 && canRotate)
        {

            transform.eulerAngles = new Vector3(0, 0, 20);
            if (!_am.GetCurrentAnimatorStateInfo(0).IsName("PC_Flight"))
            {
                _am.Play("PC_Flight");
            }

        }

        if (_rb.velocity.y < 0 && canRotate)
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

}
