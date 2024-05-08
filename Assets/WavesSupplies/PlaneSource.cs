using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSource : MonoBehaviour
{
    private WaveControl waveControler;
    public GameObject visual;
    private float currentAngle;
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
        /*Vector3 boxPosition = waveControler.transform.position;
        Vector3 direction = boxPosition - transform.position;
        Quaternion rotationTowardsBox = Quaternion.LookRotation(direction);
        visual.transform.rotation = rotationTowardsBox;*/

        
        /*Vector3 lookDirection = transform.position - waveControler.transform.position;
        Vector3 objectNormal = gameObject.transform.rotation * new Vector3(0, 0, 1);
        currentAngle = Vector3.Angle(lookDirection, objectNormal);        
        Vector3 newDirection = Vector3.RotateTowards(objectNormal, lookDirection, currentAngle, 0.1f);
        transform.rotation = Quaternion.LookRotation(newDirection);   */    

    }
}
