using UnityEngine;

public class Billboard : MonoBehaviour
{
    private float currentAngle;
    public float cutoffAngle;
    [Range(1f, 10000f)]
    public float smoothingFactor;
    public Transform targetTransform;
    private Camera mainCamera;
    private Vector3 z = new Vector3(0, 0, 1);
    private Vector3 y = new Vector3(0, 1, 0);
    private Vector3 x = new Vector3(1, 0, 0);

    void Start()
    {
        mainCamera = Camera.main;

        // Check if the main camera exists
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
        }
    }
    void Update()
    {
        Vector3 lookDirection = transform.position - targetTransform.position;
        Vector3 objectNormal = gameObject.transform.rotation * z;

        currentAngle = Vector3.Angle(lookDirection, objectNormal);
        
        if (currentAngle > cutoffAngle)
        {
            float difference = currentAngle - cutoffAngle;
            Vector3 newDirection = Vector3.RotateTowards(objectNormal, lookDirection, difference/smoothingFactor, 0.1f);
            
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }


}