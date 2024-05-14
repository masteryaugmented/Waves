using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class PointableSlider : MonoBehaviour
{
    public Transform leftFingerTip, rightFingerTip;
    public PokeInteractable pokeInteractable;
    void Start()
    {
        
        foreach (PokeInteractor poker in pokeInteractable.Interactors)
        {
            poker._pointTransform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
