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
    //public int selectedYear;
    //public ColorizeCountriesScript colorizeScript;
    public DataManager dataManager;
    public GameObject globe;
    public GlobePhotonColorControlScript globePhotonScript;
    //public PhotonView photonView;


    public GameObject globePrefab;
    //public WorldMapGlobe map;

    void Start()
    {
        //selectedYear = 2018;

        //map = WorldMapGlobe.instance;

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
            Debug.Log("Pressed N or thumb");

        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            //selectedYear = Mathf.Min(selectedYear + 1, 2018);
            globePhotonScript.SelectNextYear();
            Debug.Log("Pressed Next year ");
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            //selectedYear = Mathf.Max(selectedYear - 1, 1991);
            globePhotonScript.SelectPreviousYear();
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

        var photonGrabbable = networkedGlobe.GetComponent<PhotonGrabbableObject>();
        globe = photonGrabbable.gameObject;
        globePhotonScript = globe.GetComponent<GlobePhotonColorControlScript>();
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

    //[PunRPC]
    /*void RotateRightRPC()
    {
        step = rotateSpeed * Time.deltaTime;
        globe.transform.Rotate(0, -step, 0);
    }*/
    //**Rotation Code End**

    //**Change Year Code**
    // private void ChangeYear()
    //{
    /*  if (OVRInput.Get(OVRInput.Touch.One))
      {
          SelectNextYear();
      }
      if (OVRInput.Get(OVRInput.Touch.Two))
      {
          SelectPreviousYear();
      }*/

    /*if (!OVRInput.Get(OVRInput.Touch.One) && !OVRInput.Get(OVRInput.Touch.Two))
    {
        isInteracting = false;
        photonView.RPC("ReleaseOwnership", RpcTarget.All);
    }*/
    //}
    /*
    //[PunRPC]
    //void SelectNextYear()
    {

        selectedYear = Mathf.Min(selectedYear + 1, 2018);
        //photonView.RPC("UpdateSelectedYearRPC", RpcTarget.All, selectedYear);
        //UpdateSelectedYearRPC(selectedYear);

        Debug.Log("Selected Year: " + selectedYear);
        colorizeScript.ColorizeCountries(selectedYear, globe);
    }
    [PunRPC]
    void SelectPreviousYear()
    {
        selectedYear = Mathf.Max(selectedYear - 1, 1991);
        //photonView.RPC("UpdateSelectedYearRPC", RpcTarget.All, selectedYear);
        //UpdateSelectedYearRPC(selectedYear);

        Debug.Log("Selected Year: " + selectedYear);
        colorizeScript.ColorizeCountries(selectedYear, globe);
    }*/

    /*
    [PunRPC]
    void UpdateSelectedYearRPC(int year)
    {
        selectedYear = year;
        Debug.Log("Selected Year: " + selectedYear);
        colorizeScript.ColorizeCountries(selectedYear, globe);
    }*/

    //**Change Year Code End**

    //**Interaction and Ownership handling**
    
    //**Interaction and Ownership handling End**
}