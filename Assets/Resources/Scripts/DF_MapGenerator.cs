using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DF_MapGenerator : MonoBehaviour
{
    public GameObject mapSlot, slotContainer;
    public int mapWidth, mapHeight, mapDifficulty;
    public static DF_Class_Enemies enemiesJSON;
    Dictionary<int, List<DF_Class_EntityType>> difficultyConfig = new Dictionary<int, List<DF_Class_EntityType>>();
    int[] traderCoordinates;

    private void Start()
    {
        difficultyConfig.Add(0, new List<DF_Class_EntityType>() { new DF_Class_EntityType("Tree", 30), new DF_Class_EntityType("POI", 10), new DF_Class_EntityType("Empty", 60)});
        difficultyConfig.Add(1, new List<DF_Class_EntityType>() { new DF_Class_EntityType("Tree", 20), new DF_Class_EntityType("POI", 20), new DF_Class_EntityType("Empty", 60) });
        difficultyConfig.Add(2, new List<DF_Class_EntityType>() { new DF_Class_EntityType("Tree", 20), new DF_Class_EntityType("POI", 30), new DF_Class_EntityType("Empty", 50) });
        difficultyConfig.Add(3, new List<DF_Class_EntityType>() { new DF_Class_EntityType("Tree", 15), new DF_Class_EntityType("POI", 50), new DF_Class_EntityType("Empty", 35) });
        traderCoordinates = new int[] { Random.Range(0, mapWidth), Random.Range(0, mapHeight)};

        LoadEnemiesJSON();
        GenerateMap(mapWidth, mapHeight);
    }

    void LoadEnemiesJSON()
    {
        enemiesJSON = JsonUtility.FromJson<DF_Class_Enemies>((Resources.Load("JSON/enemies") as TextAsset).text);
    }

    void GenerateMap(int width, int height)
    {
        int slotIndex = 1;

        for (int widthIterator = 0; widthIterator < width; widthIterator++)
        {
            for (int heightIterator = 0; heightIterator < height; heightIterator++)
            {
                GameObject spawnedSlot = Instantiate(mapSlot, new Vector3(heightIterator, 0, widthIterator), Quaternion.identity);
                spawnedSlot.name = "MapSlot_" + slotIndex;
                spawnedSlot.transform.parent = slotContainer.transform;
                slotIndex++;

                if (widthIterator == width / 2 && heightIterator == height / 2)
                    GeneratePlayer(spawnedSlot.transform.GetChild(0).gameObject);
                else
                    GenerateEntity(Random.Range(0, 100), spawnedSlot.transform.GetChild(0).gameObject, new int[] { widthIterator, heightIterator});
            }
        }
    }

    void GeneratePlayer(GameObject parent)
    {
        GameObject spawnedEntity = Instantiate(Resources.Load("Prefabs/Player") as GameObject, parent.transform.position, Quaternion.identity);
        spawnedEntity.transform.parent = parent.transform;
        DF_PlayerMovementController.playerObject = spawnedEntity;
        DF_CameraController.SetMainCamera();
    }

    void GenerateEntity(int randomizedPercentage, GameObject parent, int[] currentCoordinates)
    {
        GameObject spawnedEntity;
        List<DF_Class_EntityType> entitiesWithPercentages = SetPercentageRanges(difficultyConfig[mapDifficulty]);

        if (currentCoordinates[0] == traderCoordinates[0] && currentCoordinates[1] == traderCoordinates[1])
        {
            spawnedEntity = Instantiate(Resources.Load("Prefabs/Trader") as GameObject, parent.transform.position, Quaternion.identity);
            spawnedEntity.transform.parent = parent.transform;
        }
        else
        {
            foreach (DF_Class_EntityType entity in entitiesWithPercentages)
            {

                if (randomizedPercentage >= entity.percentageRange[0] && randomizedPercentage < entity.percentageRange[1])
                {
                    if (entity.name != "Empty")
                    {
                        spawnedEntity = Instantiate(Resources.Load("Prefabs/" + entity.name) as GameObject, parent.transform.position, Quaternion.identity);
                        spawnedEntity.transform.parent = parent.transform;
                        if (entity.name == "POI")
                            DF_GameProgressController.numberOfPOIs += 1;
                        break;
                    }
                }
            }
        }
    }

    List<DF_Class_EntityType> SetPercentageRanges(List<DF_Class_EntityType> entityTypes)
    {
        int currentRange = 0;

        foreach (DF_Class_EntityType entityType in entityTypes)
        {
            entityType.percentageRange = new int[] { currentRange, currentRange + entityType.spawningChance };
            currentRange += entityType.spawningChance;
        }

        return entityTypes;
    }
}
