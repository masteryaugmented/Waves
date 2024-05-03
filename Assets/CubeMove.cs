using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    private void Start()
    {
        //change();
    }
    public void change()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + .1f, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
