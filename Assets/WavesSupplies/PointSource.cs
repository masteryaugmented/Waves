using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PointSource : MonoBehaviour
{
    public PointableSlider slider;
    [HideInInspector]
    public float kMag = 50, intensity = .01f;
    [HideInInspector]
    public Vector4 waveData;
    void Start()
    {
        WaveControl.instance.addPointSource(this);
        gameObject.transform.parent = GameObject.FindGameObjectWithTag("Simulation").transform;
    }

    // Update is called once per frame
    void Update()
    {
        setData();
    }

    private void setData()
    {
        Vector3 waveBoxPos = WaveControl.instance.gameObject.transform.localPosition;
        kMag = -100 * slider.x + 110f;
        waveData = new  Vector4(transform.localPosition.x-waveBoxPos.x, transform.localPosition.y - waveBoxPos.y, transform.localPosition.z - waveBoxPos.z, kMag);
    }

}
