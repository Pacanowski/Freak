using UnityEngine;
using System.Collections;
using UnityEngine.Assemblies;
using System.Runtime.InteropServices;

public class MapGenerator : MonoBehaviour
{

    public enum DrawMode { DrawMesh, NoiseMap, ColourMap };
    public DrawMode drawMode;

    public const int mapChunkSize = 241;
    [Range(0, 6)]
    public int levelOfDetail;

    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] biomes;

    public Transform meshPos;



    public TreeGenerator treeGen;
    public int maxTreeCount = 100;
    public int treeCount = 0;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {

        treeCount = 0;


        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < biomes.Length; i++)
                {
                    if (currentHeight <= biomes[i].height)
                    {
                        colourMap[y * mapChunkSize + x] = biomes[i].colour;
                        break;
                    }
                }

                if (currentHeight >= 0.8 && treeCount < maxTreeCount)
                {

                    float treeSpawnChance = Random.value;
                    if (treeSpawnChance > 0.95f)
                    {
                        treeGen.GenerateTree(x - 120, 50, -y + 120);
                        //Debug.Log(currentHeight);
                        treeCount++;
                    }

                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colourMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.DrawMesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(colourMap, mapChunkSize, mapChunkSize));
        }
    }

    void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}