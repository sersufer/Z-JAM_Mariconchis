using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconicBuildingMovement : MonoBehaviour
{

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.WorldToScreenPoint(transform.position).x > (Screen.width / 2))
        {
            transform.position -= Vector3.right * 5 * Time.deltaTime;
        }
    }
}
