using Photon.Pun;
using UnityEditor;
using UnityEngine;
using WPM;
using PhotonPun = Photon.Pun;
using PhotonRealtime = Photon.Realtime;

public class GlobeControllerScript : MonoBehaviour
{
    public float rotateSpeed = 100f;
    private float step;
    private float rotationY;
    public float thumbstickThreshold = 0.1f;
    public int selectedYear;
    public ColorizeCountriesScript colorizeScript;
    public DataManager dataManager;
    public WorldMapGlobe globeScript;

    public bool itemsSpawned;

    public GameObject globe;
    private int globeID;
    public GameObject globePrefab;

    public GameObject slider;
    private int sliderID;
    public GameObject sliderPrefab;

    public GameObject UI;
    private int UIID;
    public GameObject UIPrefab;

    // Raycasting
    [Header("Raycast to select country")]
    public Transform rayOrigin;
    public bool isCountryHighlighted = false;
    public int latestHighlightedCountryIndex = -1, latestTargetRegionIndex = -1;
    public string highlightedCountryName;
    int targetCountryIndex, targetRegionIndex;

    void Start()
    {
        selectedYear = 2018;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) && !itemsSpawned)
        {
            Debug.Log("Y button pressed on controller");
            SpawnGlobe();
            SpawnSlider();
            //SpawnUI();
            itemsSpawned = true;
        }

        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        if (thumbstickInput.magnitude > thumbstickThreshold || Input.GetKeyDown(KeyCode.N))
        {
            
            rotationY = thumbstickInput.x;
            RotateRPC(rotationY);
        }

        // Check if a country is highlighted
        OnTryCountrySelected();
        if(isCountryHighlighted && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("Country Selected: " + highlightedCountryName);

            OnCountrySelected(targetCountryIndex, targetRegionIndex);
            // TODO: Send info to the Data Manager UI

        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            spawnGlobeAndSlider();
        }

    }

    void spawnGlobeAndSlider()
    {
        SpawnGlobe();
        SpawnSlider();
    }


    private void SpawnGlobe()
    {
        Debug.Log("Spawning Globe");
        if (globePrefab == null)
        {
            Debug.LogError("Globe prefab is not assigned.");
            return;
        }

        var networkedGlobe = PhotonNetwork.Instantiate(globePrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
        globe = networkedGlobe.gameObject;
        globeScript = globe.GetComponent<WorldMapGlobe>();

        var photonGrabbable = networkedGlobe.GetComponent<PhotonGrabbableObject>();
        globeID = networkedGlobe.GetComponent<PhotonPun.PhotonView>().ViewID;

        if (photonGrabbable == null)
        {
            Debug.LogError("PhotonGrabbableObject component is missing on the instantiated globe.");
            return;
        }

        photonGrabbable.TransferOwnershipToLocalPlayer();
    }

    private void SpawnSlider()
    {
        Debug.Log("Spawning Slider");
        if (sliderPrefab == null)
        {
            Debug.LogError("Slider prefab is not assigned.");
            return;
        }

        var networkedSlider = PhotonNetwork.Instantiate(sliderPrefab.name, new Vector3(0.184f, -0.5f, -0.5f), Quaternion.identity);
        slider = networkedSlider.gameObject;

        var photonGrabbable = networkedSlider.GetComponent<PhotonGrabbableObject>();
        sliderID = networkedSlider.GetComponent<PhotonPun.PhotonView>().ViewID;

        if (photonGrabbable == null)
        {
            Debug.LogError("PhotonGrabbableObject component is missing on the instantiated slider.");
            return;
        }

        photonGrabbable.TransferOwnershipToLocalPlayer();
    }
    /*
    private void SpawnUI()
    {
        Debug.Log("Spawning UI");
        if (UIPrefab == null)
        {
            Debug.LogError("UI prefab is not assigned.");
            return;
        }

        var networkedUI = PhotonNetwork.Instantiate(UIPrefab.name, new Vector3(0, 1, 4), Quaternion.identity);
        UI = networkedUI.gameObject;

        var photonGrabbable = networkedUI.GetComponent<PhotonGrabbableObject>();
        UIID = networkedUI.GetComponent<PhotonPun.PhotonView>().ViewID;

        if (photonGrabbable == null)
        {
            Debug.LogError("PhotonGrabbableObject component is missing on the instantiated UI.");
            return;
        }

        photonGrabbable.TransferOwnershipToLocalPlayer();
    }*/

    [PunRPC]
    void RotateRPC(float direction)
    {
        if (globe == null)
        {
            Debug.LogError("Globe object is not instantiated.");
            return;
        }

        step = rotateSpeed * direction * Time.deltaTime;
        globe.transform.Rotate(0, step, 0);
    }

    public void updateYearForAll(int year)
    {
        gameObject.GetComponent<PhotonPun.PhotonView>().RPC("UpdateYearRPC", RpcTarget.All, year);
        UpdateYearRPC(year);
    }

    [PunRPC]
    public void UpdateYearRPC(int year)
    {
        if (globe == null)
        {
            Debug.LogError("Globe object is not instantiated.");
            return;
        }

        selectedYear = year;
        colorizeScript.ColorizeCountries(year, globe);
    }

    private void OnCountrySelected(int countryIndex, int regionIndex)
    {
        var country = globeScript.GetCountry(countryIndex);
        if (country != null)
        {
            var dataForYear = dataManager.GetDataForYear(selectedYear);
            if (dataForYear != null)
            {
                var countryData = dataForYear.Find(c => c.countryName == country.name);
                if (countryData != null)
                {
                    CanvasCountryInfoManager.instance.showInUI(country.name, countryData.co2emissions.ToString());
                }
                else
                {
                    Debug.LogWarning("No data found for country: " + country.name + " in year: " + selectedYear);
                }
            }
            else
            {
                Debug.LogWarning("No data found for year: " + selectedYear);
            }
        }
    }

    public void OnTryCountrySelected()
    {
        if (itemsSpawned)
        {
            Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

            //int targetCountryIndex, targetRegionIndex;
            isCountryHighlighted = globeScript.GetCountryIndex(ray, out targetCountryIndex, out targetRegionIndex);
            if (isCountryHighlighted)
            {

                // If the latest highlighted region changed, dehighlight the previous and highlight the new one.
                if (latestHighlightedCountryIndex != targetCountryIndex)
                {
                    // Dehighlight
                    globeScript.HideCountryRegionHighlights(false);
                    
                    // Save the latest country in global variables
                    latestHighlightedCountryIndex = targetCountryIndex;
                    latestTargetRegionIndex = targetRegionIndex;

                    // Highlight the new country
                    globeScript.ToggleCountryRegionSurfaceHighlight(latestHighlightedCountryIndex, latestTargetRegionIndex, Color.blue, true);
                    
                    // Get the name of the country
                    highlightedCountryName = globeScript.countries[targetCountryIndex].name;
                }
                //Debug.Log("Highlighted: "+ highlightedCountryName);
            }
            else
            {
                // If nothing is selected, dehighlight the latest one
                globeScript.HideCountryRegionHighlights(false);
                latestHighlightedCountryIndex = -1;
                highlightedCountryName = "";
            }
        }
    }
}