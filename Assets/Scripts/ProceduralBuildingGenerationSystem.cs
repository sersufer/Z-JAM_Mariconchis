using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBuildingGenerationSystem : MonoBehaviour
{
    private Camera gameCamera;
    public float BeginPositionX;
    public float BeginPositionY;
    private float EndPositionX;
    public Collider2D mainCameraCollider;
    public int gameVelocity;

    // Building GameObjects
    private GameObject BuildingType1;
    private GameObject BuildingType2;
    private GameObject BuildingType3;

    // variables
    private GameObject building1;
    private GameObject building2;
    private GameObject building3;

    public bool CanSpawnNextBuilding;

    // Start is called before the first frame update
    void Start()
    {
        gameVelocity = 30;
        gameCamera = Camera.main;
        mainCameraCollider = gameObject.GetComponent<Collider2D>();
        BeginPositionY = mainCameraCollider.offset.y; //gameCamera.pixelHeight;
        //BeginPositionX = gameCamera.pixelWidth;
        BeginPositionX = mainCameraCollider.bounds.max.x;
        EndPositionX = 0;
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

        switch (typeOfBuilding)
        {
            case 1:

                building1 = Resources.Load<GameObject>("Prefabs/Buildings/Building1");
                xOffsetBuilding = building1.GetComponent<Collider2D>().bounds.max.x;
                spriteHeight = building1.GetComponent<SpriteRenderer>().size.y;
                //building1 = GameObject.Instantiate(building1, new Vector3(BeginPositionX, 0, 0), Quaternion.identity);
                //building1 = GameObject.Instantiate(building1, new Vector3(BeginPositionX + xOffsetBuilding, 0, 0), Quaternion.identity);
                building1 = GameObject.Instantiate(building1, new Vector3(BeginPositionX + xOffsetBuilding, BeginPositionY, 0), Quaternion.identity);

                //building1 = GameObject.Instantiate(building1, new Vector3(BeginPositionX + xOffsetBuilding, 0, 0), Quaternion.identity);
                building1.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                break;

            case 2:

                building2 = Resources.Load<GameObject>("Prefabs/Buildings/Building2");
                xOffsetBuilding = building2.GetComponent<Collider2D>().bounds.max.x;
                spriteHeight = building2.GetComponent<SpriteRenderer>().size.y;
                //building1 = GameObject.Instantiate(building1, new Vector3(BeginPositionX, 0, 0), Quaternion.identity);
                //building1 = GameObject.Instantiate(building1, new Vector3(BeginPositionX + xOffsetBuilding, 0, 0), Quaternion.identity);
                building2 = GameObject.Instantiate(building2, new Vector3(BeginPositionX + xOffsetBuilding, BeginPositionY, 0), Quaternion.identity);

                //building1 = GameObject.Instantiate(building1, new Vector3(BeginPositionX + xOffsetBuilding, 0, 0), Quaternion.identity);
                building2.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                break;

            case 3:

                building3 = Resources.Load<GameObject>("Prefabs/Buildings/Building3");
                xOffsetBuilding = building3.GetComponent<Collider2D>().bounds.max.x;
                spriteHeight = building3.GetComponent<SpriteRenderer>().size.y;
                //building1 = GameObject.Instantiate(building1, new Vector3(BeginPositionX, 0, 0), Quaternion.identity);
                //building1 = GameObject.Instantiate(building1, new Vector3(BeginPositionX + xOffsetBuilding, 0, 0), Quaternion.identity);
                building3 = GameObject.Instantiate(building3, new Vector3(BeginPositionX + xOffsetBuilding, BeginPositionY, 0), Quaternion.identity);

                //building1 = GameObject.Instantiate(building1, new Vector3(BeginPositionX + xOffsetBuilding, 0, 0), Quaternion.identity);
                building3.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * gameVelocity);
                break;

            default:
                break;
        }

        CanSpawnNextBuilding = false;
    }
    #endregion
}
