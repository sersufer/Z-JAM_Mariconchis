using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBuildingGenerationSystem : MonoBehaviour
{
    #region Properties
    // Building GameObjects
    private GameObject BuildingType1;
    private GameObject BuildingType2;
    private GameObject BuildingType3;
    private GameObject TestCube;

    // variables
    private GameObject building1;
    private GameObject building2;
    private GameObject building3;

    public bool CanSpawnNextBuilding;

    [Header("Camera related properties")]
    private Camera gameCamera;
    private Vector2 ScreenBounds;
    public float ScreenBorderPositionX;
    private float EndPositionX;
    public Collider2D mainCameraCollider;
    public int gameVelocity;
    

    [Header("Floor related")]
    public float BeginPositionY;
    public GameObject floorGameObject;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameVelocity = 10;

        //  Test cube
        TestCube = GameObject.Find("TestCube");

        //  Camera related variables
        gameCamera = Camera.main;
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        mainCameraCollider = gameObject.GetComponent<Collider2D>();
        ////BeginPositionY = mainCameraCollider.offset.y; //gameCamera.pixelHeight; VIEJO
        ////ScreenBorderPositionX = mainCameraCollider.bounds.max.x; //ScreenBorderPositionX = gameCamera.pixelWidth; VIEJO
        ScreenBorderPositionX = mainCameraCollider.bounds.size.x / 2;
        EndPositionX = -ScreenBounds.x;

        //  Floor related variables
        floorGameObject = GameObject.FindGameObjectWithTag("Floor");
        BeginPositionY = -ScreenBounds.y; //+ floorGameObject.GetComponent<Collider2D>().bounds.size.y;

        CanSpawnNextBuilding = false;
        SpawnBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSpawnNextBuilding)
        {
            SpawnBuilding();
        }
    }

    #region My methods

    void SpawnBuilding()
    {
        int typeOfBuilding = (int)Random.RandomRange(0, 3);

        float xOffsetBuilding = 0;
        float spriteHeight = 0;

#warning TypeOfBuildingVariable forced to 1 (TESTING)
        typeOfBuilding = 1;
        switch (typeOfBuilding)
        {
            case 1:

                building1 = Resources.Load<GameObject>("Prefabs/Buildings/Building1");
                ////xOffsetBuilding = building1.GetComponent<Collider2D>().bounds.max.x; VIEJO
                xOffsetBuilding = building1.GetComponent<SpriteRenderer>().bounds.size.x;
                float xSpawnPosition = ScreenBorderPositionX + (building1.GetComponent<Collider2D>().bounds.size.x / 2);

                ////spriteHeight = building1.GetComponent<Collider2D>().bounds.size.y;
                //spriteHeight = building1.GetComponent<Collider2D>().bounds.center.y / 2;

                //float ySpawnPosition = BeginPositionY + spriteHeight;
                float ySpawnPosition = BeginPositionY - building1.GetComponent<Collider2D>().bounds.size.y / 2;
                
                //building1 = GameObject.Instantiate(building1, new Vector3(xSpawnPosition, ySpawnPosition, 0), Quaternion.identity); //este aparece a mitad
                //float pruebas = floorGameObject.GetComponent<SpriteRenderer>().bounds.size.y - floorGameObject.GetComponent<Collider2D>().bounds.center.y;
                //building1 = GameObject.Instantiate(building1, new Vector3(xSpawnPosition, BeginPositionY + pruebas, 0), Quaternion.identity);
                
                //building1 = GameObject.Instantiate(building1, new Vector3(xSpawnPosition, BeginPositionY - building1.GetComponent<Collider2D>().bounds.size.y, 0), Quaternion.identity);
                building1 = GameObject.Instantiate(building1, new Vector3(xSpawnPosition, BeginPositionY - building1.GetComponent<Collider2D>().bounds.center.y, 0), Quaternion.identity);
                ///building1 = GameObject.Instantiate(building1, new Vector3(xSpawnPosition + building1.GetComponent<Collider2D>().bounds.size.x / 2, BeginPositionY + building1.GetComponent<Collider2D>().bounds.size.y, 0), Quaternion.identity); // este aparece a mitad

                //building1 = GameObject.Instantiate(building1, new Vector3(ScreenBorderPositionX + xOffsetBuilding, 0, 0), Quaternion.identity);
                building1.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                break;

            case 2:

                building2 = Resources.Load<GameObject>("Prefabs/Buildings/Building2");
                xOffsetBuilding = building2.GetComponent<Collider2D>().bounds.max.x;
                
                spriteHeight = building2.GetComponent<SpriteRenderer>().size.y;
                //building1 = GameObject.Instantiate(building1, new Vector3(ScreenBorderPositionX, 0, 0), Quaternion.identity);
                //building1 = GameObject.Instantiate(building1, new Vector3(ScreenBorderPositionX + xOffsetBuilding, 0, 0), Quaternion.identity);
                building2 = GameObject.Instantiate(building2, new Vector3(ScreenBorderPositionX + xOffsetBuilding, BeginPositionY, 0), Quaternion.identity);

                //building1 = GameObject.Instantiate(building1, new Vector3(ScreenBorderPositionX + xOffsetBuilding, 0, 0), Quaternion.identity);
                building2.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                break;

            case 3:

                building3 = Resources.Load<GameObject>("Prefabs/Buildings/Building3");
                xOffsetBuilding = building3.GetComponent<Collider2D>().bounds.max.x;
                spriteHeight = building3.GetComponent<SpriteRenderer>().size.y;
                //building1 = GameObject.Instantiate(building1, new Vector3(ScreenBorderPositionX, 0, 0), Quaternion.identity);
                //building1 = GameObject.Instantiate(building1, new Vector3(ScreenBorderPositionX + xOffsetBuilding, 0, 0), Quaternion.identity);
                building3 = GameObject.Instantiate(building3, new Vector3(ScreenBorderPositionX + xOffsetBuilding, BeginPositionY, 0), Quaternion.identity);

                //building1 = GameObject.Instantiate(building1, new Vector3(ScreenBorderPositionX + xOffsetBuilding, 0, 0), Quaternion.identity);
                building3.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                break;

            case 4:

                //GameObject.Instantiate(TestCube, new Vector3(ScreenBounds.x, BeginPositionY, 0), Quaternion.identity);
                //GameObject.Instantiate(TestCube, new Vector3(ScreenBounds.x, BeginPositionY + TestCube.GetComponent<Collider2D>().bounds.size.y / 2, 0), Quaternion.identity);
                //GameObject.Instantiate(TestCube, new Vector3(ScreenBounds.x, floorGameObject.GetComponent<Collider2D>().offset.y + (TestCube.GetComponent<Collider2D>().bounds.size.y / 2), 0), Quaternion.identity);

                ////GameObject.Instantiate(TestCube, new Vector3(ScreenBounds.x, floorGameObject.transform.lossyScale.y + TestCube.GetComponent<Collider2D>().bounds.size.y / 2, 0), Quaternion.identity);
                ////Debug.Log(floorGameObject.transform.lossyScale.y);
                ////Debug.Log(GameObject.Find("Square").transform.localPosition.y);


                //var prueba = floorGameObject.GetComponent<Collider2D>().bounds.center.y;
                var prueba = floorGameObject.GetComponent<Collider2D>().bounds.size.y / 2 - floorGameObject.GetComponent<Collider2D>().bounds.center.y;
                GameObject.Instantiate(TestCube, new Vector3(ScreenBounds.x, BeginPositionY, 0), Quaternion.identity);
                
                break;

            default:
                break;
        }

        CanSpawnNextBuilding = false;
    }
    #endregion
}
