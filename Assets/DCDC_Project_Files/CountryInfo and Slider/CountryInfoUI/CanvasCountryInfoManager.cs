using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using PhotonPun = Photon.Pun;

public class CanvasCountryInfoManager : MonoBehaviour
{
    public GameObject prefabCountryContainer;
    public Transform countryInfoContainer;
    public int maxNumberOfCountriesDisplayed = 3;
    public GameObject info;



    private int numberOfCountriesDisplayed = 0;

    // SINGLETON
    public static CanvasCountryInfoManager instance;
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if(countryInfoContainer != null)
        {
            numberOfCountriesDisplayed = countryInfoContainer.childCount;
        }
        

        //Check if it should show description
        if(numberOfCountriesDisplayed ==0)
            info.SetActive(true);
        else
            info.SetActive(false);

        ///// TESTING!!
        //if(Input.GetKeyDown(KeyCode.I))
        //{
        //    ShowNewCountryInCanvas("Iran", "45.93");
        //}
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    ShowNewCountryInCanvas("China", "2784.444");
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    ShowNewCountryInCanvas("Sweden", "5.6");
        //}
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowNewCountryInCanvas("Sweden", "6.0");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ShowNewCountryInCanvas("Iran", "5.0");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowNewCountryInCanvas("Sweden", "6.0");
        }

    }

    public void ShowNewCountryInCanvas(string countryName, string co2ratio)
    {
        if(numberOfCountriesDisplayed < maxNumberOfCountriesDisplayed)
        {
            GameObject go = Instantiate(prefabCountryContainer, countryInfoContainer);
            SingleCountryDataUI dataScript = go.GetComponent<SingleCountryDataUI>();

            //
            gameObject.GetComponent<PhotonPun.PhotonView>().RPC("dataScriptSetData", RpcTarget.All, countryName, co2ratio);
            //dataScript.SetData(countryName, co2ratio);
        }
    }

}
