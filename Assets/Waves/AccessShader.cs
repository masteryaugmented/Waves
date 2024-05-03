using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessShader : MonoBehaviour
{
    private GameObject targetGameObject; // Assign the target game object in the Inspector

    private void Start()
    {
        // Get the Renderer component of the target game object
        Renderer targetRenderer = gameObject.GetComponent<Renderer>();

        if (targetRenderer == null)
        {
            Debug.LogError("Target game object does not have a Renderer component!");
            return;
        }

        // Get the Material attached to the Renderer
        Material targetMaterial = targetRenderer.material;

        // Access the shader of the Material
        Shader targetShader = targetMaterial.shader;
        Debug.Log(targetShader.GetPropertyCount());

        // Use the shader for further processing or modification
        // For example, you can change properties of the shader or switch to a different shader.

        // Example: Change the color of the target material
        targetMaterial.SetFloat("_Wavenumber", 1000f);
        targetMaterial.SetFloatArray("_Source", new float[] { 1f, 0f, 0f });
    }
}