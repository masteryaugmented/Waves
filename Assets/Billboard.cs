using UnityEngine;

public class Billboard : MonoBehaviour
{
    private float currentAngle, cutoffAngle;
    private Camera mainCamera;

    void Start()
    {
        cutoffAngle = 15f;
        mainCamera = Camera.main;

        // Check if the main camera exists
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
        }
    }
    void Update()
    {
        Vector3 lookDirection = transform.position - mainCamera.transform.position;
        Vector3 objectNormal = gameObject.transform.rotation * new Vector3(0, 0, 1);

        currentAngle = Vector3.Angle(lookDirection, objectNormal);
        
        if (currentAngle > cutoffAngle)
        {
            float difference = currentAngle - cutoffAngle;
            Vector3 newDirection = Vector3.RotateTowards(objectNormal, lookDirection, difference/50f, 0.1f);
            
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            Debug.LogError("Main camera not found!");
        }
    }


}