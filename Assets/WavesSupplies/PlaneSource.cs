using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlaneSource : MonoBehaviour
{
    [HideInInspector]
    public float kMag = 50, intensity = .01f;
    [HideInInspector]
    public Vector4 waveData;
    void Start()
    {
        WaveControl.instance.planeSources.Add(this);
        gameObject.transform.parent = GameObject.FindGameObjectWithTag("Simulation").transform;
    }

    // Update is called once per frame
    void Update()
    {
        setData();
    }
    
    private void setData()
    {        
        Vector3 normalizedK = kMag * (WaveControl.instance.transform.localPosition - gameObject.transform.localPosition).normalized;
        waveData = new Vector4(normalizedK.x, normalizedK.y, normalizedK.z, intensity);
    }
}
