using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    private float screenRightBorderX;
    private float spriteRightBorderX;
    private float screenBottomBorderY;
    private float spriteLeftBorderX;
    private bool newBackgroundHasBeenAlreadyInstantiated;
    public Transform subject;
    Vector2 startPosition;
    float startZ;
    float xMovement;
    //Vector2 travel => (Vector2) cam.transform.position - startPosition;
    //Vector2 travel => startPosition - new Vector2(xMovement, startPosition.x);
    Vector2 travel => new Vector2(xMovement, startPosition.y) - startPosition;
    float distanceFromSubject => transform.position.z - subject.position.z;
    

    float clipingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parrallaxFactor => Mathf.Abs(distanceFromSubject / clipingPlane);

    void Start()
    {
        newBackgroundHasBeenAlreadyInstantiated = false;

        cam = Camera.main;
        screenRightBorderX = GameObject.Find("Main Camera").GetComponent<ProceduralBuildingGenerationSystem>().ScreenBounds.x;
        screenBottomBorderY = -GameObject.Find("Main Camera").GetComponent<ProceduralBuildingGenerationSystem>().ScreenBounds.y;
        spriteRightBorderX = GetComponent<SpriteRenderer>().bounds.max.x;
        spriteLeftBorderX = GetComponent<SpriteRenderer>().bounds.min.x;

        subject = GameObject.Find("Player").transform;
        startPosition = transform.position;
        xMovement = startPosition.x;
        startZ = transform.position.z;

        GameObject.Find("TestCube").transform.position = new Vector3(spriteRightBorderX, transform.position.y, transform.position.z - 1);
    }

    void Update()
    {
        xMovement -= 0.05f;

        Vector2 newPos = startPosition + travel; //* parrallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);

        if (GetComponent<SpriteRenderer>().bounds.max.x < spriteLeftBorderX)
        {
            Destroy(this.gameObject);
            //this.gameObject = duplicateGameObject;
        }
        if (!newBackgroundHasBeenAlreadyInstantiated)
        {
            if (GetComponent<SpriteRenderer>().bounds.max.x <= screenRightBorderX) //spriteRightBorderX <= screenRightBorderX
            {
                //transform.position = startPosition;
                ///var duplicateGameObject = GameObject.Instantiate(this.gameObject, new Vector3(screenRightBorderX, screenBottomBorderY, -16), Quaternion.identity);

                ///duplicateGameObject.AddComponent<Parallax>();

                newBackgroundHasBeenAlreadyInstantiated = true;
            }
        }


    }
    /*
     void Start()
    {
        cam = Camera.main;
        screenRightBorderX = GameObject.Find("Main Camera").GetComponent<ProceduralBuildingGenerationSystem>().ScreenBounds.x;
        screenBottomBorderY = -GameObject.Find("Main Camera").GetComponent<ProceduralBuildingGenerationSystem>().ScreenBounds.y;
    spriteRightBorderX = GetComponent<SpriteRenderer>().bounds.max.x;
        spriteLeftBorderX = GetComponent<SpriteRenderer>().bounds.min.x;

        subject = GameObject.Find("Player").transform;
        startPosition = transform.position;
        xMovement = startPosition.x;
        startZ = transform.position.z;

        GameObject.Find("TestCube").transform.position = new Vector3(spriteRightBorderX, transform.position.y, transform.position.z - 1);
    }

    void Update()
    {
        xMovement -= 0.05f;

        Vector2 newPos = startPosition + travel * parrallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
        if(transform.position.x < spriteLeftBorderX)
        {
            
            //this.gameObject = duplicateGameObject;
        }
        if(spriteRightBorderX <= screenRightBorderX)
        {
            //transform.position = startPosition;
            var duplicateGameObject = GameObject.Instantiate(this.gameObject, new Vector3(screenRightBorderX, screenBottomBorderY, -16), Quaternion.identity);
            duplicateGameObject.AddComponent<Parallax>();

            //Poner el borde izquierdo del sprite en el borde derecho de la pantalla
            //transform.position.x = 
        }
        
    }
     */

}
