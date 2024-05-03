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
        trainToWaveVolume();
    }

    private void trainToWaveVolume()
    {
        Vector3 boxPosition = waveControler.transform.position;
        Vector3 direction = boxPosition - transform.position;
        Quaternion rotationTowardsBox = Quaternion.LookRotation(direction);
        visual.transform.rotation = rotationTowardsBox;
    }
}
