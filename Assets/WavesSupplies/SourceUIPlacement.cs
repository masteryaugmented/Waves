using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceUIPlacement : MonoBehaviour
{
    Vector3 boxPosition;
    void Start()
    {
        boxPosition = WaveControl.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position + 0.2f*(transform.parent.position - boxPosition).normalized;
    }
}
