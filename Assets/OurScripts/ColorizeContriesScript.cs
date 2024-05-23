using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WPM;
using static OVRPlugin;

public class ColorizeCountriesScript : MonoBehaviour
{
    public DataManager dataManager;

    public GameObject redCube;
    public GameObject redCubeTwo;
    public GameObject blueCube;

    List<DataFormatWorld> data;

    PhotonView photonView;

    private int yearVar;
    //public WorldMapGlobe map;
    
    //public GlobeControllerScript globeController;
    //int selectedYear;

    void Start()
    {
        
    }

    
    
    public void ColorizeCountries(int year, GameObject globe)
    {
        yearVar = year;
        Debug.Log("This is the first line within ColorizeCountries method in the Colorize Coutries script");

        Debug.Log("This is almost last line but BEFORE RED CUBE within ColorizeCountries method in the Colorize Coutries script, all colours should have been applied now.");

        var networkedredCube = PhotonNetwork.Instantiate(redCube.name, new Vector3(-1, 1, 0), Quaternion.identity);
        Debug.Log("This is almost last line but AFTER RED CUBE within ColorizeCountries method in the Colorize Coutries script, all colours should have been applied now.");


        //WorldMapGlobe globeScript = globe.GetComponent<WorldMapGlobe>();
        //photonView = globe.GetComponent<PhotonView>();
        //List<DataFormatWorld> data = dataManager.GetDataForYear(yearVar);



        StartCoroutine(letDataBeRead(yearVar, globe));

        

    }

    private Color CalculateColor(float value, float min, float max)
    {
        float normalized = (value - min) / (max - min);
        return Color.Lerp(Color.white, Color.red, normalized);
    }

    IEnumerator letDataBeRead(int yearVar, GameObject globe)
    {
        yield return new WaitUntil(() => dataManager.dataPerYear != null);

        data = dataManager.GetDataForYear(yearVar);
        if (data == null)
        {
            Debug.LogError("Data is null for year " + yearVar);
            yield break;
        }

        yield return new WaitForSeconds(2);

        var globeScript = globe.GetComponent<WorldMapGlobe>();
        float minCO2 = float.MaxValue;
        float maxCO2 = float.MinValue;

        foreach (DataFormatWorld entry in data)
        {
            if (entry.co2emissions < minCO2) minCO2 = entry.co2emissions;
            if (entry.co2emissions > maxCO2) maxCO2 = entry.co2emissions;
        }

        foreach (DataFormatWorld entry in data)
        {
            Color color = CalculateColor(entry.co2emissions, minCO2, maxCO2);
            globeScript.ToggleCountrySurface(entry.countryName, true, color);
        }

        var networkedblueCube = PhotonNetwork.Instantiate(blueCube.name, new Vector3(1, 1, 0), Quaternion.identity);
    }




    /*IEnumerator letDataBeRead (int yearVar, GameObject globe)
    {
        data = dataManager.GetDataForYear(yearVar);
        yield return new WaitForSeconds(2);
        //PhotonView PV = photonView;
        Debug.Log("Before second red cube after datamanager load in");

        var networkedredCubeTwo = PhotonNetwork.Instantiate(redCube.name, new Vector3(-2, 1, 0), Quaternion.identity);


        var globeScript = globe.GetComponent<WorldMapGlobe>();
        if (data == null)
        {
            Debug.LogError("No data available for year " + yearVar);
            //return;
        }

        float minCO2 = float.MaxValue;
        float maxCO2 = float.MinValue;

        foreach (DataFormatWorld entry in data)
        {
            if (entry.co2emissions < minCO2) minCO2 = entry.co2emissions;
            if (entry.co2emissions > maxCO2) maxCO2 = entry.co2emissions;
        }

        foreach (DataFormatWorld entry in data)
        {
            Color color = CalculateColor(entry.co2emissions, minCO2, maxCO2);
            //Applying the appropriate color to the contry
            //PV.GetComponent<WorldMapGlobe>();
            globeScript.ToggleCountrySurface(entry.countryName, true, color);

        }

        Debug.Log("This is almost last line but BEFORE BLUE CUBE within ColorizeCountries method in the Colorize Coutries script, all colours should have been applied now.");
        var networkedblueCube = PhotonNetwork.Instantiate(blueCube.name, new Vector3(1, 1, 0), Quaternion.identity);
        Debug.Log("This is the the last line AFTER BLUE CUBE within ColorizeCountries method in the Colorize Coutries script, all colours should have been applied now.");


    }*/




}