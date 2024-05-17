using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WPM;
using Photon.Pun;

public class GlobeControllerScript : MonoBehaviourPun // Inherit from MonoBehaviourPun
{
    public DataManager dataManager;
    public float rotateSpeed; // degrees per second
    float step;
    public ColorizeCountriesScript colorizeScript;
    public int selectedYear;

    // Start is called before the first frame update
    void Start()
    {
        colorizeScript.map.ToggleCountrySurface("Brazil", true, Color.blue);
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return; // If it's not the local player's object, don't execute the input code

        HandleInput();
        HandleRotation();
    }

    void HandleInput()
    {
        // Handle color change input
        if (OVRInput.GetUp(OVRInput.RawButton.Y) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            photonView.RPC("ColorizeCountriesRPC", RpcTarget.All, selectedYear); // Call RPC to colorize countries for all players
            Debug.Log("Hello from inside the if condition");
        }

        // Handle year selection logic
        if (OVRInput.GetUp(OVRInput.RawButton.A))
        {
            SelectPreviousYear();
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.B))
        {
            SelectNextYear();
        }
    }

    void HandleRotation()
    {
        step = rotateSpeed * Time.deltaTime;

        if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft))
        {
            RotateLeft();
        }

        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight))
        {
            RotateRight();
        }
    }

    void SelectPreviousYear()
    {
        selectedYear = Mathf.Max(selectedYear - 1, 1991); // Adjust year bounds as needed
        photonView.RPC("UpdateSelectedYearRPC", RpcTarget.All, selectedYear); // Call RPC to update selected year for all players
    }

    void SelectNextYear()
    {
        selectedYear = Mathf.Min(selectedYear + 1, 2018); // Adjust year bounds as needed
        photonView.RPC("UpdateSelectedYearRPC", RpcTarget.All, selectedYear); // Call RPC to update selected year for all players
    }

    [PunRPC]
    void UpdateSelectedYearRPC(int year)
    {
        selectedYear = year;
        Debug.Log("Selected Year: " + selectedYear);
        colorizeScript.ColorizeCountries(selectedYear);
    }

    [PunRPC]
    void ColorizeCountriesRPC(int year)
    {
        Debug.Log("Colorizing countries for year: " + year);
        colorizeScript.ColorizeCountries(year);
    }

    void RotateLeft()
    {
        transform.Rotate(0, step, 0);
        photonView.RPC("RotateLeftRPC", RpcTarget.Others); // Send RPC to other clients
        Debug.Log("Right Thumbstick detected - Left");
    }

    void RotateRight()
    {
        transform.Rotate(0, -step, 0);
        photonView.RPC("RotateRightRPC", RpcTarget.Others); // Send RPC to other clients
        Debug.Log("Right Thumbstick detected - Right");
    }

    [PunRPC]
    void RotateLeftRPC()
    {
        transform.Rotate(0, step, 0);
    }

    [PunRPC]
    void RotateRightRPC()
    {
        transform.Rotate(0, -step, 0);
    }
}