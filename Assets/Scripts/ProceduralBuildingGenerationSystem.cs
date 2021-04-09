using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBuildingGenerationSystem : MonoBehaviour
{
    private Camera gameCamera;
    private float BeginPositionX;
    private float EndPositionX;

    // Building GameObjects
    private GameObject BuildingType1;
    private GameObject BuildingType2;
    private GameObject BuildingType3;

    // variables
    private GameObject building1;
    private GameObject building2;
    private GameObject building3;
    private GameObject building4;

    private bool CanSpawnNextBuilding;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = Camera.main;
        BeginPositionX = gameCamera.pixelWidth;
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
        switch (typeOfBuilding)
        {
            case 1:
                BuildingType1 = null;
                //BuildingType1 = GameObject.Find()
                //Resources.Load<GameObject>("Prefabs/Abilities/Arrow");
                BuildingType1 = Instantiate(Building1, new Vector3(), Quaternion.identity);
                //Building1.gameObject.transform.position.x


                break;
            case 2:
                GameObject.Instantiate(Building2);
                break;
            case 3:
                GameObject.Instantiate(Building3);
                break;
            default:
                break;
        }

        CanSpawnNextBuilding = false;
    }
    #endregion
}
