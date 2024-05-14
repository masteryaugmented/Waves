using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaveControl : MonoBehaviour
{
    public List<PlaneSource> planeSources;
    private List<string> sourceNamesShader;
    private Material waveMaterial;
    private int planeSourceCount;
    public static WaveControl instance;
    private PhotonView pv;
    private void Awake()
    {
        waveMaterial = gameObject.GetComponent<Renderer>().material;
        waveMaterial.SetVector("_ObjectScale", gameObject.transform.localScale);
        planeSourceCount = 0;
        instance = this;
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (planeSourceCount > 0)
        {
            setPlaneWaves();
        }
    }

    private void setPlaneWaves()
    {     
        for(int i=0; i< planeSourceCount; i++)
        {
            Vector4 waveData = planeSources[i].waveData;
            string propertyName = string.Format("_PlaneSource{0}", i.ToString());
            waveMaterial.SetVector(propertyName, waveData);
        }
    }

    //called by button
    public void newPlaneSource()
    {
        if(planeSourceCount >= 2)
        {
            return;
        }
        Vector3 localOffset = new Vector3(1f, 0f, 0f);
        Vector3 spawnPoint = gameObject.transform.TransformPoint(gameObject.transform.localPosition + localOffset);
        PhotonNetwork.Instantiate("PlaneSource", spawnPoint, gameObject.transform.rotation);
    }

    //called by spawned source
    public void addPlaneSource(PlaneSource newSource)
    {
        planeSources.Add(newSource);
        planeSourceCount++;
        waveMaterial.SetInt("_PlaneSourceCount", planeSourceCount);
    }

}
