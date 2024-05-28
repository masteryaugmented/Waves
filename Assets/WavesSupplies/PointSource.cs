using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Oculus.Interaction;

public class PointSource : MonoBehaviour
{
    public PointableSlider slider;
    [HideInInspector]
    public float kMag = 50, intensity = .01f;
    [HideInInspector]
    public Vector4 waveData;
    protected Grabbable grabbable;

    void Start()
    {
        WaveControl.instance.addPointSource(this);
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
        WaveControl.instance.removePointSource(this);
    }

    private void setData()
    {
        Vector3 waveBoxPos = WaveControl.instance.gameObject.transform.localPosition;
        kMag = -100 * slider.x + 110f;
        waveData = new  Vector4(transform.localPosition.x-waveBoxPos.x, transform.localPosition.y - waveBoxPos.y, transform.localPosition.z - waveBoxPos.z, kMag);
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
                    if (Vector3.Magnitude(gameObject.transform.position - WaveControl.instance.trashCan.transform.position) < 0.2f)
                    {
                        PhotonNetwork.Destroy(this.gameObject);
                    }

                }
                break;
        }
    }

}
