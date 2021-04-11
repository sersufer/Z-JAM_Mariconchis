using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBuildingGenerationSystem : MonoBehaviour
{

    #region Properties
    
    private GameObject TestCube;

    public enum GamePhase
    {
        First,
        Second,
        Final
    }

    public GamePhase gamePhase;

    // Object variables
    public GameObject LastSpawnedObject;
    private GameObject assetBuildingResource;
    private GameObject building1;
    private GameObject building2;
    private GameObject building3;
    private GameObject building4;
    private GameObject building5;
    private GameObject building6;
    private GameObject iconicBuilding1;
    private GameObject iconicBuilding2;
    private GameObject iconicBuilding3;
    private GameObject iconicBuilding4;
    private GameObject iconicBuilding5;
    private GameObject iconicBuilding6;
    public bool IsIconicBuilding;
    public int IconicBuildingCounter;
    private GameObject finalLevelBuilding;
    public string discoveredFinalBuilding;
    private GameObject trashBin;
    private GameObject streetLight;
    private GameObject cat;
    private Transform playerTransform;
    private float nextFloorObstacleSpawnTime;
    private int hasEnteredHereOnce = 0;
    public bool CanSpawnNextBuilding;

    [Header("Camera related properties")]
    private Camera gameCamera;
    public Vector2 ScreenBounds;
    public float ScreenBorderPositionX;
    private float EndPositionX;
    public Collider2D mainCameraCollider;
    public int gameVelocity;
    public float nextXAvailableSpawnPosition;
    

    [Header("Floor related")]
    public float BeginPositionY;
    public GameObject floorGameObject;

    private float timeLeft;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gamePhase = GamePhase.First;
        PreLoadResources();
        gameVelocity = 70;
        IsIconicBuilding = false;
        //  Test cube
        //TestCube = GameObject.Find("TestCube");

        playerTransform = GameObject.Find("Player").transform;

        //  Camera related variables
        gameCamera = Camera.main;
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        mainCameraCollider = gameObject.GetComponent<Collider2D>();
        
        ScreenBorderPositionX = mainCameraCollider.bounds.size.x / 2;
        EndPositionX = -ScreenBounds.x;

        //  Floor related variables
        floorGameObject = GameObject.FindGameObjectWithTag("Floor");
        BeginPositionY = -ScreenBounds.y + 0.6f; 

        CanSpawnNextBuilding = false;
        nextFloorObstacleSpawnTime = Time.time + Random.Range(1, 3);
        SpawnBuilding();
        timeLeft = Time.time + 60;
    }

    // Update is called once per frame
    void Update()
    {


        if (Time.time > nextFloorObstacleSpawnTime && gamePhase != GamePhase.Final)
        {
            int randomItemProp = (int) Random.Range(1, 2);
            switch (randomItemProp)
            {
                case 1:
                    streetLight = Resources.Load<GameObject>("Prefabs/Obstacles/StreetLight");
                    
                    //var auxStreetLight = 
                    var auxStreetLight = GameObject.Instantiate(streetLight, new Vector3(ScreenBounds.x, BeginPositionY, playerTransform.position.z), Quaternion.identity);
                    auxStreetLight.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                    break;

                //case 3:
                //    cat = Resources.Load<GameObject>("Prefabs/Obstacles/Cat");
                //    //var auxCat = 
                //    var auxCat = GameObject.Instantiate(cat, new Vector3(ScreenBounds.x, BeginPositionY, playerTransform.position.z), Quaternion.identity);
                //    auxCat.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                //    break;

                case 2:
                    trashBin = Resources.Load<GameObject>("Prefabs/Obstacles/TrashBin");
                    
                    //var auxTrashBin = 
                    var auxTrashBin = GameObject.Instantiate(trashBin, new Vector3(ScreenBounds.x, BeginPositionY, playerTransform.position.z), Quaternion.identity);
                    auxTrashBin.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                    break;
                    
            }
            nextFloorObstacleSpawnTime = Time.time + Random.Range(6, 10);

        }
        //if (Time.time >= 6)
        //{
        //    gamePhase = GamePhase.Final;

        //}else if (Time.time >= 3)
        //{
        //    gamePhase = GamePhase.Second;
        //}
        if (Time.time >= timeLeft)
        {
            gamePhase = GamePhase.Final;

        }

        if (CanSpawnNextBuilding)
        {

            if (gamePhase == GamePhase.Final)
            {
                if(finalLevelBuilding == null)
                {
                    ++hasEnteredHereOnce;
                    if(hasEnteredHereOnce == 1)
                    {
                        StartCoroutine(GenerateEndLevelBuilding());
                    }else if (hasEnteredHereOnce >= 1)
                    {
                        if(finalLevelBuilding != null)
                        {
                            if(finalLevelBuilding.transform.position.x == gameCamera.GetComponent<BoxCollider2D>().bounds.center.x)
                            {
                                finalLevelBuilding.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                            }
                        }
                    }
                }

            }
            else
            {
                if (finalLevelBuilding == null)
                    SpawnBuilding();
            }

        }

        //if (CanSpawnNextBuilding)
        //{

        //    if (gamePhase == GamePhase.Final)
        //    {
        //        StartCoroutine(GenerateEndLevelBuilding());

        //    }
        //    else
        //    {
        //        if (finalLevelBuilding == null)
        //            SpawnBuilding();
        //    }

        //}
    }

    #region My methods

    IEnumerator GenerateEndLevelBuilding()
    {
        yield return new WaitForSeconds(6);
        //finalLevelBuilding = Resources.Load<GameObject>("Prefabs/Buildings/BasilicaDelPilar");

        //finalLevelBuilding = GameObject.Instantiate(finalLevelBuilding, new Vector3(ScreenBounds.x, BeginPositionY, 0), Quaternion.identity);
        //LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);

        
        int randomIconicBuilding = (int)Random.Range(1, 6);
        switch (randomIconicBuilding)
        {
            case 1:
                finalLevelBuilding = Resources.Load<GameObject>("Prefabs/Buildings/IconicBuilding_Seo");

                
                LastSpawnedObject = GameObject.Instantiate(finalLevelBuilding, new Vector3(ScreenBounds.x, BeginPositionY, 0), Quaternion.identity);
                LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                
                break;

            case 2:
                finalLevelBuilding = Resources.Load<GameObject>("Prefabs/Buildings/IconicBuilding_Aljaferia");
                
                LastSpawnedObject = GameObject.Instantiate(finalLevelBuilding, new Vector3(ScreenBounds.x, BeginPositionY, 0), Quaternion.identity);
                LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                
                break;

            case 3:
                finalLevelBuilding = Resources.Load<GameObject>("Prefabs/Buildings/IconicBuilding_TeatroPrincipal");

                LastSpawnedObject = GameObject.Instantiate(finalLevelBuilding, new Vector3(ScreenBounds.x, BeginPositionY, 0), Quaternion.identity);
                LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                
                break;

            case 4:

                finalLevelBuilding = Resources.Load<GameObject>("Prefabs/Buildings/IconicBuilding_ElPlata");

                LastSpawnedObject = GameObject.Instantiate(finalLevelBuilding, new Vector3(ScreenBounds.x, BeginPositionY, 0), Quaternion.identity);
                LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                
                break;

            case 5:

                finalLevelBuilding = Resources.Load<GameObject>("Prefabs/Buildings/IconicBuilding_MercadoCentral");
                
                LastSpawnedObject = GameObject.Instantiate(finalLevelBuilding, new Vector3(ScreenBounds.x, BeginPositionY, 0), Quaternion.identity);
                LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                
                break;

            case 6:
                finalLevelBuilding = Resources.Load<GameObject>("Prefabs/Buildings/BasilicaDelPilar");

                LastSpawnedObject = GameObject.Instantiate(finalLevelBuilding, new Vector3(ScreenBounds.x, BeginPositionY, 0), Quaternion.identity);
                LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                
                break;
        }

        discoveredFinalBuilding = finalLevelBuilding.name;


    }

    void PreLoadResources()
    {
        //trashBin = Resources.Load<GameObject>("Prefabs/Obstacles/TrashBin");
        //streetLight = Resources.Load<GameObject>("Prefabs/Obstacles/StreetLight");
        //cat = Resources.Load<GameObject>("Prefabs/Obstacles/Cat");
    }

    void SpawnBuilding()
    {
        int typeOfBuilding = 1;
        int randomPosibilityToSpawnEnemies = (int)Random.Range(1, 2);

        switch (gamePhase)
        {
            case GamePhase.First:
                typeOfBuilding = (int)Random.RandomRange(0, 6);
                break;

            case GamePhase.Second:
                //typeOfBuilding = (int)Random.RandomRange(0, 7);
                break;

            case GamePhase.Final:
                //typeOfBuilding = 8;
                break;
        }

        float xOffsetBuilding = 0;
        float spriteHeight = 0;
        float xSpawnPosition;
        //string assetPrefabRoute;

        switch (typeOfBuilding)
        {
            case 1:
                switch (randomPosibilityToSpawnEnemies)
                {
                    case 1:
                        building1 = Resources.Load<GameObject>("Prefabs/Buildings/Building1");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + building1.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(building1, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(building1, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;

                    case 2:
                        building1 = Resources.Load<GameObject>("Prefabs/Buildings/Building1WindowCleaner");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + building1.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(building1, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(building1, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;
                }

                break;

            case 2:
                switch (randomPosibilityToSpawnEnemies)
                {
                    case 1:
                        building2 = Resources.Load<GameObject>("Prefabs/Buildings/Building2");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + building2.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(building2, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(building2, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;

                    case 2:
                        building2 = Resources.Load<GameObject>("Prefabs/Buildings/Building2WithBroom");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + building2.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(building2, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(building2, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;
                }
                
                
                break;

            case 3:

                building3 = Resources.Load<GameObject>("Prefabs/Buildings/Building2");

                if (LastSpawnedObject)
                {
                    xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + building3.GetComponent<Collider2D>().bounds.size.x;
                    LastSpawnedObject = GameObject.Instantiate(building3, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                    LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                }
                else
                {
                    xSpawnPosition = ScreenBounds.x;
                    LastSpawnedObject = GameObject.Instantiate(building3, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                    LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                }
                break;

            case 4:

                switch (randomPosibilityToSpawnEnemies)
                {
                    case 1:

                        building4 = Resources.Load<GameObject>("Prefabs/Buildings/Building4");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + building4.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(building4, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(building4, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;

                    case 2:

                        building4 = Resources.Load<GameObject>("Prefabs/Buildings/Building4WomanWithPan");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + building4.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(building4, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(building4, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;
                }
                
                break;

            case 5:
                building5 = Resources.Load<GameObject>("Prefabs/Buildings/Building5");

                if (LastSpawnedObject)
                {
                    xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + building5.GetComponent<Collider2D>().bounds.size.x;
                    LastSpawnedObject = GameObject.Instantiate(building5, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                    LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                }
                else
                {
                    xSpawnPosition = ScreenBounds.x;
                    LastSpawnedObject = GameObject.Instantiate(building5, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                    LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                }
                break;

            case 6:

                switch (randomPosibilityToSpawnEnemies)
                {
                    case 1:
                        building6 = Resources.Load<GameObject>("Prefabs/Buildings/Building6");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + building6.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(building6, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(building6, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;

                    case 2:
                        building6 = Resources.Load<GameObject>("Prefabs/Buildings/Building6WomanPan&Broomer");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + building6.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(building6, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(building6, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;
                }
                
                break;

            /*
            case 666:
                int randomIconicBuilding = (int)Random.Range(1, 6);
                switch (randomIconicBuilding)
                {
                    case 1:
                        iconicBuilding1 = Resources.Load<GameObject>("Prefabs/Buildings/IconicBuilding_Seo");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + iconicBuilding1.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(iconicBuilding1, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(iconicBuilding1, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;

                    case 2:
                        iconicBuilding2 = Resources.Load<GameObject>("Prefabs/Buildings/IconicBuilding_Aljaferia");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + iconicBuilding2.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(iconicBuilding2, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(iconicBuilding2, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;

                    case 3:
                        iconicBuilding3 = Resources.Load<GameObject>("Prefabs/Buildings/IconicBuilding_TeatroPrincipal");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + iconicBuilding2.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(iconicBuilding3, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(iconicBuilding3, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;

                    case 4:

                        iconicBuilding4 = Resources.Load<GameObject>("Prefabs/Buildings/IconicBuilding_ElPlata");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + iconicBuilding2.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(iconicBuilding4, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(iconicBuilding4, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;

                    case 5:

                        iconicBuilding4 = Resources.Load<GameObject>("Prefabs/Buildings/IconicBuilding_MercadoCentral");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + iconicBuilding2.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(iconicBuilding4, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(iconicBuilding4, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;

                    case 6:
                        iconicBuilding4 = Resources.Load<GameObject>("Prefabs/Buildings/BasilicaDelPilar");

                        if (LastSpawnedObject)
                        {
                            xSpawnPosition = LastSpawnedObject.transform.position.x + LastSpawnedObject.GetComponent<Collider2D>().bounds.size.x + iconicBuilding2.GetComponent<Collider2D>().bounds.size.x;
                            LastSpawnedObject = GameObject.Instantiate(iconicBuilding4, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);

                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        else
                        {
                            xSpawnPosition = ScreenBounds.x;
                            LastSpawnedObject = GameObject.Instantiate(iconicBuilding4, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
                            LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                        }
                        break;
                }
                
                break;
            */

            //case 8:
            //    finalLevelBuilding = Resources.Load<GameObject>("Prefabs/Buildings/BasilicaDelPilar");

            //    xSpawnPosition = ScreenBounds.x;
            //    LastSpawnedObject = GameObject.Instantiate(finalLevelBuilding, new Vector3(xSpawnPosition, BeginPositionY, 0), Quaternion.identity);
            //    LastSpawnedObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
            //    break;
        }

        CanSpawnNextBuilding = false;
    }
    #endregion
}
