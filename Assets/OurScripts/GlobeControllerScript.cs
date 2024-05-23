using Photon.Pun;
using UnityEngine;
using WPM;
using PhotonPun = Photon.Pun;
using PhotonRealtime = Photon.Realtime;

public class GlobeControllerScript : MonoBehaviour //PunCallbacks
{
    //private bool isInteracting = false;
    public float rotateSpeed = 100f;
    private float step;
    private float rotationY;
    public float thumbstickThreshold = 0.1f;  // Tröskelvärde för att avgöra om thumbsticken används
    public int selectedYear;
    public ColorizeCountriesScript colorizeScript;
    //public DataManager dataManager;
    public GameObject globe;
    //public WorldMapGlobe globeScript;
    //public GlobePhotonColorControlScript globePhotonScript;
    //public PhotonView photonView;

    private int globeID;


    public GameObject globePrefab;
    //public WorldMapGlobe map;

    void Start()
    {

    }


    void Update()
    {

        // Kontrollera om trigger på kontrollen trycks ner Spawna isåfall globen, lägg till block för om en redan finns.
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("Y button pressed on controller");
            SpawnGlobe();
        }

        //Get thumbstick input för rotation
        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        //Check to see that its above threshhold, eg being used
        if (thumbstickInput.magnitude > thumbstickThreshold || Input.GetKeyDown(KeyCode.N))
        {
            //photonView.RPC("RotateLeftRPC", RpcTarget.Others);
            rotationY = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;
            RotateRPC(rotationY);
            //Debug.Log("Pressed N or thumb");

        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            //selectedYear = Mathf.Min(selectedYear + 1, 2018);
            SelectNextYear();
            Debug.Log("Pressed Next year ");
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            //selectedYear = Mathf.Max(selectedYear - 1, 1991);
            SelectPreviousYear();
            Debug.Log("Pressed prev year ");
        }
    }

    private void SpawnGlobe()
    {
        Debug.Log("Went to method");
        if (globePrefab == null)
        {
            Debug.LogError("Globe prefab is not assigned.");
            return;
        }

        var networkedGlobe = PhotonNetwork.Instantiate(globePrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log("Instantiated Globe");
        globe = networkedGlobe.gameObject;
        //Pho
        var photonGrabbable = networkedGlobe.GetComponent<PhotonGrabbableObject>();
        globeID = networkedGlobe.GetComponent<PhotonPun.PhotonView>().ViewID;

        //globePhotonScript = globe.GetComponent<GlobePhotonColorControlScript>();
        selectedYear = 2018;
        //photonView = networkedGlobe.GetComponent<PhotonView>();

        if (photonGrabbable == null)
        {
            Debug.LogError("PhotonGrabbableObject component is missing on the instantiated globe.");
            return;
        }

        photonGrabbable.TransferOwnershipToLocalPlayer();
        Debug.Log("Ownership transferred to local player.");
    }

    [PunRPC]
    void RotateRPC(float direction)
    {
        step = rotateSpeed * direction * Time.deltaTime;
        globe.transform.Rotate(0, step, 0);
    }




    //These two methods need to be changed to get year from SLIDER and then use the ** Marked line** only
    public void SelectNextYear()
    {
        Debug.Log("Pressed to select next year");
        //selectedYear = Mathf.Min(selectedYear + 1, 2018);

        //***
        gameObject.GetComponent <PhotonPun.PhotonView>().RPC("UpdateYearRPC", RpcTarget.All, selectedYear);
        //***

        Debug.Log("This is after the RPC call next year");
    }

    public void SelectPreviousYear()
    {
        Debug.Log("Pressed to select previous year");
        //selectedYear = Mathf.Max(selectedYear - 1, 1991);
        
        gameObject.GetComponent<PhotonPun.PhotonView>().RPC("UpdateYearRPC", RpcTarget.All, selectedYear);
        Debug.Log("This is after the RPC call prev year");
        //UpdateYearRPC(selectedYear);
        //colorizeCountriesScript.ColorizeCountries(selectedYear, this.gameObject);
        //photonView.RPC("colorizeScript.ColorizeCountries", RpcTarget.All, selectedYear, globe);
    }

    //Change above methods for SLIDER logic





    [PunRPC]
    public void UpdateYearRPC(int year)
    {
        Debug.Log("This is the first line within RPC method UpdateYearRPC");
        GameObject myGlobe = PhotonPun.PhotonView.Find(globeID).gameObject;
        Debug.Log("Updated to: " + selectedYear);
        colorizeScript.ColorizeCountries(year, myGlobe);
        //photonView.RPC("colorizeScript.ColorizeCountries", RpcTarget.All, selectedYear);
        Debug.Log("This is the last line within RPC method UpdateYearRPC");
    }



    //**Interaction and Ownership handling**

    //**Interaction and Ownership handling End**
}