using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Oculus.Interaction;

public class PlaneSource : MonoBehaviour
{
    public PointableSlider slider;
    [HideInInspector]
    public float kMag = 50, intensity = .01f;
    [HideInInspector]
    public Vector4 waveData;
    protected Grabbable grabbable;
    void Start()
    {
        WaveControl.instance.addPlaneSource(this);
        //WaveControl.instance.planeSources.Remove(this);
        gameObject.transform.parent = GameObject.FindGameObjectWithTag("Simulation").transform;
        grabbable = GetComponent<Grabbable>();
        grabbable.WhenPointerEventRaised += OnPointerEventRaised; 
    }

    // Update is called once per frame
    void Update()
    {
        setData();
    }

    private void OnDestroy()
    {
        WaveControl.instance.removePlaneSource(this);
    }

    private void setData()
    {
        kMag = -100 * slider.x + 110f;
        Vector3 normalizedK = kMag * (WaveControl.instance.transform.localPosition - gameObject.transform.localPosition).normalized;
        waveData = new Vector4(normalizedK.x, normalizedK.y, normalizedK.z, intensity);
    }

    virtual public void OnPointerEventRaised(PointerEvent pointerEvent)
    {
        switch (pointerEvent.Type)
        {
            case PointerEventType.Select:
                if (grabbable.SelectingPointsCount == 1)
                {
                    
                }
                break;
            case PointerEventType.Unselect:
                if (grabbable.SelectingPointsCount == 0)
                {
                    if(Vector3.Magnitude(gameObject.transform.position - WaveControl.instance.trashCan.transform.position) < 0.2f)
                    {
                        PhotonNetwork.Destroy(this.gameObject);
                    }
                    
                }
                break;
        }
    }
}
