using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        //newPlaneSource();
    }

    // Update is called once per frame
    void Update()
    {
        setPlaneWaves();
    }

    private void setPlaneWaves()
    {
        float scale = 8f;
        Vector3 normalizedK = -scale*planeSources[0].transform.localPosition.normalized;        
        Vector4 waveData = new Vector4(normalizedK.x, normalizedK.y, normalizedK.z, .002f);
        waveData = waveData * scale;
        waveMaterial.SetVector("_PlaneSource1", waveData);
        waveMaterial.SetVector("_ObjectScale", gameObject.transform.localScale);
    }

    public void newPlaneSource()
    {
        Vector3 spawnPoint = gameObject.transform.TransformPoint(new Vector3(.25f, 0f, 0f));
        PhotonNetwork.Instantiate("PlaneSource", spawnPoint, gameObject.transform.rotation).transform.parent=gameObject.transform.parent.transform;
    }
}
