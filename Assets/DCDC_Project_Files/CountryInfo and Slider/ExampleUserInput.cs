using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleUserInput : MonoBehaviour
{
    //public CanvasCountryInfoManager canvasInfoManagerScript;


    // Update is called once per frame
    void Update()
    {
        /// TESTING!!
        if (Input.GetKeyDown(KeyCode.I) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            CanvasCountryInfoManager.instance.ShowNewCountryInCanvas("Iran", "45.93");
        }
        if (Input.GetKeyDown(KeyCode.C) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            CanvasCountryInfoManager.instance.ShowNewCountryInCanvas("China", "2784.444");
        }
        if (Input.GetKeyDown(KeyCode.S) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            CanvasCountryInfoManager.instance.ShowNewCountryInCanvas("Sweden", "5.6");
        }
    }
}
