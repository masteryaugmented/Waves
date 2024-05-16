using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class PointableSlider : MonoBehaviour
{
    private List<Transform> fingerTips;
    public Transform sliderKnob, panel;
    public float minimumZ;
    [HideInInspector]
    public float x, y;
    private float xRange, yRange;
    void Start()
    {
        fingerTips = new List<Transform>();
        fingerTips.Add(GameObject.FindGameObjectWithTag("LeftIndexTip").transform);
        fingerTips.Add(GameObject.FindGameObjectWithTag("RightIndexTip").transform);
        xRange = panel.transform.localScale.x / 2;
        yRange = panel.transform.localScale.y / 2;
    }

    void Update()
    {
        checkForFinger();
        setExternalValues();
    }
            
    private void checkForFinger()
    {
        if(fingerTips.Count == 0) { return; }
        foreach (Transform fingerTip in fingerTips)
        {
            Vector3 fingerTipLocalPosition = transform.InverseTransformPoint(fingerTip.position);
            //if fingerWithinDistance(fingerTipLocalPosition)

            if (fingerWithinDistance(fingerTipLocalPosition))
            {
                SampleController.Instance.Log("scaledZ"+(fingerTipLocalPosition.z * transform.lossyScale.z).ToString());
                SampleController.Instance.Log("scaledX"+(fingerTipLocalPosition.x * transform.lossyScale.x).ToString());


                // if fingertip is to the right 
                if(fingerTipLocalPosition.x > xRange)
                {
                    SampleController.Instance.Log("beyond right bound");
                    sliderKnob.transform.localPosition = Vector3.right * xRange;
                    return;
                }

                // if fingertip is to the left
                if (fingerTipLocalPosition.x < -xRange)
                {
                    SampleController.Instance.Log("beyond left bound");
                    sliderKnob.transform.localPosition = Vector3.left * xRange;
                    return;
                }
                //if fingertip is in range
                sliderKnob.transform.localPosition = Vector3.right * fingerTipLocalPosition.x;
                return;
            }
            SampleController.Instance.Log("Finger out of distance.");
        }
    }

    private bool fingerWithinDistance(Vector3 localPos)
    {
        float xBuffer = 0.3f;
        float yBuffer = 0.1f;
        if (Mathf.Abs(localPos.z * transform.lossyScale.z) > minimumZ) {return false;}
        if (Mathf.Abs(localPos.x) > xRange +xBuffer) {return false;}
        if (Mathf.Abs(localPos.y) > yRange + yBuffer) {return false;}
        return true;
    }

    private void setExternalValues()
    {
        x = 0.5f * (sliderKnob.transform.localPosition.x) + 0.5f;
        y = 0.5f * (sliderKnob.transform.localPosition.y) + 0.5f;
    }


}
