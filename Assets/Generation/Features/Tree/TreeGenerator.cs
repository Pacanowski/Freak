using UnityEngine;

public class TreeGenerator : MonoBehaviour
{

    public GameObject TreePrefab;
    public Transform map;

    public LayerMask groundLayer;

    public void GenerateTree(float posX, float posZ)
    {

        if (!(posX > 110 || posX < -110 || posZ > 110 || posZ < -110))
        {
            Ray ray = new Ray(new Vector3(posX, 50, posZ), Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
            {
                //Debug.Log(hit.point.y);
                GameObject tree = Instantiate(TreePrefab);
                tree.transform.SetParent(map);
                tree.transform.position = new Vector3(posX, hit.point.y - 1, posZ);
            }
        }
    }
}
