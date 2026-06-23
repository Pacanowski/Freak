using UnityEngine;

public class TreeGenerator : MonoBehaviour
{

    public GameObject TreePrefab;
    public Transform map;

    public void GenerateTree(float posX, float posY, float posZ)
    {

        if (posX > 110 || posX < -110 || posZ > 110 || posZ < -110)
        {
            //tree out of bounds
        }
        else
        {
            GameObject tree = GameObject.Instantiate(TreePrefab);
            tree.transform.SetParent(map);
            tree.transform.position = new Vector3(posX, posY, posZ);
        }



    }
}
