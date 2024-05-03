using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControl : MonoBehaviour
{
    public List<PlaneSource> planeSources;
    private List<string> sourceNamesShader;
    private Material waveMaterial;
    private int sourceCount;
    public static WaveControl instance;
    private void Awake()
    {
        waveMaterial = gameObject.GetComponent<Renderer>().material;
        sourceCount = 1;
        instance = this;        
    }

    // Update is called once per frame
    void Update()
    {
        setPlaneWaves();
    }

    private void setPlaneWaves()
    {
        Vector3 sourceLocalPosition = planeSources[0].gameObject.transform.InverseTransformPoint(transform.position);
        Vector4 waveData = new Vector4(sourceLocalPosition.x, sourceLocalPosition.y, sourceLocalPosition.z, .01f);
        waveMaterial.SetVector("_PlaneSource1", waveData);
    }
}
