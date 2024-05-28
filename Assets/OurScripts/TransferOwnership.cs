using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TransferOwnership : MonoBehaviourPun
{

    private float thumbstickThreshold = 0.1f;
    // Update is called once per frame
    void Update()
    {
        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        if (thumbstickInput.magnitude > thumbstickThreshold || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            base.photonView.RequestOwnership();
        }
    }
}
