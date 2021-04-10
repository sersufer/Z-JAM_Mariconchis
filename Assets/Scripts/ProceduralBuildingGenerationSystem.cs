using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBuildingGenerationSystem : MonoBehaviour
{

    #region Properties
    
    private GameObject TestCube;

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
    public bool IsIconicBuilding;
    public int IconicBuildingCounter;
    private GameObject finalLevelBuilding;
    private GameObject trashBin;
    private GameObject streetLight;
    private GameObject cat;
    private Transform playerTransform;
    private float nextFloorObstacleSpawnTime;

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

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        PreLoadResources();
        gameVelocity = 30;
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
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextFloorObstacleSpawnTime)
        {
            int randomItemProp = (int) Random.Range(1, 3);
            switch (randomItemProp)
            {
                case 1:
                    trashBin = Resources.Load<GameObject>("Prefabs/Obstacles/TrashBin");
                    
                    //var auxTrashBin = 
                    var auxTrashBin = GameObject.Instantiate(trashBin, new Vector3(ScreenBounds.x, BeginPositionY, playerTransform.position.z), Quaternion.identity);
                    auxTrashBin.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                    break;

                case 2:
                    streetLight = Resources.Load<GameObject>("Prefabs/Obstacles/StreetLight");
                    
                    //var auxStreetLight = 
                    var auxStreetLight = GameObject.Instantiate(streetLight, new Vector3(ScreenBounds.x, BeginPositionY, playerTransform.position.z), Quaternion.identity);
                    auxStreetLight.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                    break;

                case 3:
                    cat = Resources.Load<GameObject>("Prefabs/Obstacles/Cat");
                    //var auxCat = 
                    var auxCat = GameObject.Instantiate(cat, new Vector3(ScreenBounds.x, BeginPositionY, playerTransform.position.z), Quaternion.identity);
                    auxCat.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                    break;
            }
            nextFloorObstacleSpawnTime = Time.time + Random.Range(6, 10);

        }

        if (CanSpawnNextBuilding)
        {
            SpawnBuilding();
        }
    }

    #region My methods

    void PreLoadResources()
    {
        //trashBin = Resources.Load<GameObject>("Prefabs/Obstacles/TrashBin");
        //streetLight = Resources.Load<GameObject>("Prefabs/Obstacles/StreetLight");
        //cat = Resources.Load<GameObject>("Prefabs/Obstacles/Cat");
    }

    void SpawnBuilding()
    {
        int typeOfBuilding = (int)Random.RandomRange(0, 6);

        float xOffsetBuilding = 0;
        float spriteHeight = 0;
        float xSpawnPosition;
        //string assetPrefabRoute;

#warning TypeOfBuildingVariable forced to 1 (TESTING)
        switch (typeOfBuilding)
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

            
        }

        CanSpawnNextBuilding = false;
    }
    #endregion
}
