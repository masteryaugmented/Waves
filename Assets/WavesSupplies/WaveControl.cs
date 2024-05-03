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
        
    }
}
