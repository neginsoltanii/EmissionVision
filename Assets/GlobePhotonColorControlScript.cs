using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobePhotonColorControlScript : MonoBehaviour
{
    public int selectedYear;
    public GlobeControllerScript globeControllerScript;
    public ColorizeCountriesScript colorizeCountriesScript;
    public PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        selectedYear = 2018;
        photonView = GetComponent<PhotonView>();
        var globeController = FindAnyObjectByType<GlobeControllerScript>();
        globeControllerScript = globeController.GetComponent<GlobeControllerScript>();
        var colorizeCountries = FindAnyObjectByType<ColorizeCountriesScript>();
        colorizeCountriesScript = colorizeCountries.GetComponent<ColorizeCountriesScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void SelectNextYear()
    {
        selectedYear = Mathf.Min(selectedYear + 1, 2018);
        Debug.Log("Selected Year: " + selectedYear);
        photonView.RPC("UpdateYearRPC", RpcTarget.All, selectedYear);
        //colorizeCountriesScript.ColorizeCountries(selectedYear, this.gameObject);
    }
    
    public void SelectPreviousYear()
    {
        selectedYear = Mathf.Max(selectedYear - 1, 1991);
        Debug.Log("Selected Year: " + selectedYear);
        photonView.RPC("UpdateYearRPC", RpcTarget.All, selectedYear);
        //colorizeCountriesScript.ColorizeCountries(selectedYear, this.gameObject);
    }

    [PunRPC]
    public void UpdateYearRPC(int year)
    {
        Debug.Log("Updated to: " + selectedYear);
        colorizeCountriesScript.ColorizeCountries(year, this.gameObject);
    }

}
