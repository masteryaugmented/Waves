using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSource : MonoBehaviour
{
    private WaveControl waveControler;
    public GameObject visual;
    void Start()
    {
        waveControler = WaveControl.instance;
        waveControler.planeSources.Add(this);
        //waveControler.planeSources.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
       
    }   
}
