using UnityEngine;

public class SortingCTLGrass : MonoBehaviour
{
    public Material grassMaterial; // Assign your grass material in the Inspector
    public Camera mainCamera;
    public float distanceThreshold = 10f; // Adjust for your needs

    private int nearQueue = 2000; // Geometry render queue (opaque)
    private int farQueue = 3000; // Transparent render queue (back-to-front rendering)

    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        // Calculate the distance from the camera to the grass object
        float distance = Vector3.Distance(mainCamera.transform.position, transform.position);

        // Change render queue dynamically
        if (distance < distanceThreshold)
        {
            if (grassMaterial.renderQueue != nearQueue)
                grassMaterial.renderQueue = nearQueue;
        }
        else
        {
            if (grassMaterial.renderQueue != farQueue)
                grassMaterial.renderQueue = farQueue;
        }
    }
}
