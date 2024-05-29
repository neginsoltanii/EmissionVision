using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TransferOwnership : MonoBehaviourPun
{

    public float thumbstickThreshold = 0.1f;
    // Update is called once per frame
    void Update()
    {
        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            base.photonView.RequestOwnership();
        }
    }
}
