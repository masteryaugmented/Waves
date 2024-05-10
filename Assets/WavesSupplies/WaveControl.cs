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
        planeSourceCount = 0;
        instance = this;
        pv = GetComponent<PhotonView>();
        //newPlaneSource();
    }

    // Update is called once per frame
    void Update()
    {
        setPlaneWaves();
    }

    private void setPlaneWaves()
    {       
        Vector4 waveData = planeSources[0].waveData;
        waveMaterial.SetVector("_PlaneSource1", waveData);
        waveMaterial.SetVector("_ObjectScale", gameObject.transform.localScale);
    }

    public void newPlaneSource()
    {
        Vector3 localOffset = new Vector3(1f, 0f, 0f);
        Vector3 spawnPoint = gameObject.transform.TransformPoint(gameObject.transform.localPosition + localOffset);
        PhotonNetwork.Instantiate("PlaneSource", spawnPoint, gameObject.transform.rotation);
        planeSourceCount++;
    }

    [PunRPC]
    private void newPlaneSourceRPC()
    {
        Vector3 spawnPoint = gameObject.transform.TransformPoint(new Vector3(-.75f, 0f, 0f));
        PhotonNetwork.Instantiate("PlaneSource", spawnPoint, gameObject.transform.rotation).transform.parent=gameObject.transform.parent.transform;
        planeSourceCount++;
    }


}
