using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using PhotonPun = Photon.Pun;
using System;

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
        if (countryInfoContainer != null)
        {
            numberOfCountriesDisplayed = countryInfoContainer.childCount;
        }

        //Check if it should show description
        if (numberOfCountriesDisplayed == 0)
            info.SetActive(true);
        else
            info.SetActive(false);

        // Test Inputs
        if (Input.GetKeyDown(KeyCode.I))
        {
            int testYear = 2018;
            string testCountry = "Sweden";
            float testValueCo2 = DataManager.instance.GetCo2FromYearAndCountry(testYear, testCountry);
            gameObject.GetComponent<PhotonPun.PhotonView>().RPC("ShowNewCountryInCanvas", RpcTarget.All, testCountry, testValueCo2.ToString());
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            gameObject.GetComponent<PhotonPun.PhotonView>().RPC("ShowNewCountryInCanvas", RpcTarget.All, "Iran", "5.0");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameObject.GetComponent<PhotonPun.PhotonView>().RPC("ShowNewCountryInCanvas", RpcTarget.All, "Sweden", "6.0");
        }
    }

    public void showNewCountries(String countryName, String co2Emissions)
    {
        //CanvasCountryInfoManager.instance.ShowNewCountryInCanvas(countryName, co2Emissions);
        GetComponent<PhotonPun.PhotonView>().RPC("ShowNewCountryInCanvas", RpcTarget.AllViaServer, countryName, co2Emissions);
    }

    [PunRPC]
    public void ShowNewCountryInCanvas(string countryName, string co2ratio)
    {
        if (numberOfCountriesDisplayed < maxNumberOfCountriesDisplayed)
        {
            GameObject go = PhotonNetwork.Instantiate(prefabCountryContainer.name, Vector3.zero, Quaternion.identity, 0);
            go.transform.SetParent(countryInfoContainer, false);
            SingleCountryDataUI dataScript = go.GetComponent<SingleCountryDataUI>();

            dataScript.SetData(countryName, co2ratio);
        }
    }
}










/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using PhotonPun = Photon.Pun;
using System;

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
        if (countryInfoContainer != null)
        {
            numberOfCountriesDisplayed = countryInfoContainer.childCount;
        }

        //Check if it should show description
        if (numberOfCountriesDisplayed == 0)
            info.SetActive(true);
        else
            info.SetActive(false);

        // Test Inputs
        if (Input.GetKeyDown(KeyCode.I))
        {
            int testYear = 2018;
            string testCountry = "Sweden";
            float testValueCo2 = DataManager.instance.GetCo2FromYearAndCountry(testYear, testCountry);
            gameObject.GetComponent<PhotonPun.PhotonView>().RPC("ShowNewCountryInCanvas", RpcTarget.All, testCountry, testValueCo2.ToString());
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            gameObject.GetComponent<PhotonPun.PhotonView>().RPC("ShowNewCountryInCanvas", RpcTarget.All, "Iran", "5.0");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameObject.GetComponent<PhotonPun.PhotonView>().RPC("ShowNewCountryInCanvas", RpcTarget.All, "Sweden", "6.0");
        }
    }

    public void showNewCountries(String countryName, String co2Emissions)
    {
        //CanvasCountryInfoManager.instance.ShowNewCountryInCanvas(countryName, co2Emissions);
        GetComponent<PhotonPun.PhotonView>().RPC("ShowNewCountryInCanvas", RpcTarget.AllViaServer, countryName, co2Emissions);
    }

    [PunRPC]
    public void ShowNewCountryInCanvas(string countryName, string co2ratio)
    {
        if (numberOfCountriesDisplayed < maxNumberOfCountriesDisplayed)
        {
            GameObject go = Instantiate(prefabCountryContainer, countryInfoContainer);
            SingleCountryDataUI dataScript = go.GetComponent<SingleCountryDataUI>();

            dataScript.SetData(countryName, co2ratio);
        }
    }
}*/

