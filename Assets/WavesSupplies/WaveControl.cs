using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaveControl : MonoBehaviour
{
    
    public List<PlaneSource> planeSources;
    public List<PointSource> pointSources;
    private Material waveMaterial;
    public int planeSourceCount, pointSourceCount;
    public static WaveControl instance;
    private void Awake()
    {
        waveMaterial = gameObject.GetComponent<Renderer>().material;
        waveMaterial.SetVector("_ObjectScale", gameObject.transform.localScale);
        planeSourceCount = 0;
        pointSourceCount = 0;
        instance = this;
        SampleController.Instance.Log(PhotonNetwork.LocalPlayer.ActorNumber.ToString());
        newPlaneSource();
    }

    // Update is called once per frame
    void Update()
    {
        if (planeSourceCount > 0)
        {
            setPlaneWaves();
            
        }
        if (pointSourceCount > 0)
        {
            setPointWaves();
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

    private void setPointWaves()
    {
       for (int i = 0; i < pointSourceCount; i++)
        {
            string propertyName = string.Format("_PointSource{0}", i.ToString());
            waveMaterial.SetVector(propertyName, pointSources[i].waveData);
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

    public void newPointSource()
    {
        if (pointSourceCount >= 2)
        {
            return;
        }
        Vector3 localOffset = new Vector3(-1f, 0f, 0f);
        Vector3 spawnPoint = gameObject.transform.TransformPoint(gameObject.transform.localPosition + localOffset);
        PhotonNetwork.Instantiate("PointSource", spawnPoint, gameObject.transform.rotation);
    }

    //called by spawned source
    public void addPointSource(PointSource newSource)
    {
        pointSources.Add(newSource);
        pointSourceCount++;
        waveMaterial.SetInt("_PointSourceCount", pointSourceCount);
    }

}
