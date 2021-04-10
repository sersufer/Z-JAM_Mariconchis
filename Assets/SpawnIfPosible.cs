using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIfPosible : MonoBehaviour
{
    private GameObject buildingGameObject;
    private ProceduralBuildingGenerationSystem proceduralBuildingGenerationSystem;

    // Start is called before the first frame update
    void Start()
    {
        buildingGameObject = null;
        proceduralBuildingGenerationSystem = gameObject.GetComponentInParent<ProceduralBuildingGenerationSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            var screenBoundsX = proceduralBuildingGenerationSystem.ScreenBounds.x;
            var screenBoundsY = proceduralBuildingGenerationSystem.ScreenBounds.y;

            proceduralBuildingGenerationSystem.CanSpawnNextBuilding = true;
            //proceduralBuildingGenerationSystem.nextXAvailableSpawnPosition = screenBoundsX + collision.gameObject.GetComponent<Collider2D>().bounds.size.x * 2;
            

        }
    }
    
}
