using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ThreeDSlider : MonoBehaviour
{
    public GlobeControllerScript globeControllerScript;
    int year;

    // Start is called before the first frame update
    void Start()
    {
        year = 2018;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("YearCollider")) //
        {
            year = int.Parse(other.name);
            Debug.Log("Chose year: " + year.ToString());
            globeControllerScript.updateYearForAll(year);
        }
    }



}
