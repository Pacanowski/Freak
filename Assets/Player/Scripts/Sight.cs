using UnityEngine;

public class Sight : MonoBehaviour
{

    public Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = gameObject.transform.position;
    }
}
