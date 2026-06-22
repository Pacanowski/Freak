using UnityEngine;

public class WorldManager : MonoBehaviour
{

    MapGenerator mapGen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mapGen.drawMode = MapGenerator.DrawMode.DrawMesh;
        mapGen.GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
