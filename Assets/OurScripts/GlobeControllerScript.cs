using UnityEngine;
using WPM;
using PhotonPun = Photon.Pun;
using PhotonRealtime = Photon.Realtime;

public class GlobeInteraction : MonoBehaviour //PunCallbacks
{
    private bool isInteracting = false;
    public float rotateSpeed = 100f;
    private float step;
    private int selectedYear;
    public ColorizeCountriesScript colorizeScript;
    public DataManager dataManager;
    public GameObject globe;

    public GameObject globePrefab;
    //public WorldMapGlobe map;

    void Start()
    {
        selectedYear = 2018;

        Debug.Log("BEFOREPassed spawnglobe method");
        SpawnGlobe();
        Debug.Log("Passed spawnglobe method");

        //map = WorldMapGlobe.instance;

        // TEST
        //PhotonNetwork.Instantiate("GlobePrefab", Vector3.zero, Quaternion.identity);

    }

    private void SpawnGlobe()
    {
        Debug.Log("Went to method");
        var networkedGlobe = PhotonPun.PhotonNetwork.Instantiate(globePrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
        var photonGrabbable = networkedGlobe.GetComponent<PhotonGrabbableObject>();
        photonGrabbable.TransferOwnershipToLocalPlayer();
    }

    void Update()
    {/*
        if (photonView.IsMine && isInteracting)
        {
            HandleRotation();
            ChangeYear();
        }
        else
        {
            CheckForInteraction();
        }*/
        //}

        //**Rotation code**
        //private void HandleRotation()
        //{
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft) || Input.GetKeyDown(KeyCode.N))
        {
            //photonView.RPC("RotateLeftRPC", RpcTarget.Others);
            RotateLeftRPC();
            Debug.Log("Pressed N Left");

        }

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight) || Input.GetKeyUp(KeyCode.M))
        {
            //photonView.RPC("RotateRightRPC", RpcTarget.Others);
            RotateRightRPC();
            Debug.Log("Pressed M Right");

        }

        if (OVRInput.Get(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.K))
        {
            SelectNextYear();
            Debug.Log("Pressed Next year K");
        }
        if (OVRInput.Get(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.J))
        {
            SelectPreviousYear();
            Debug.Log("Pressed prev year J");
        }

        /*if (!OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft) && !OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
        {
            isInteracting = false;
            photonView.RPC("ReleaseOwnership", RpcTarget.All);
        }*/
    }

    //[PunRPC]
    void RotateLeftRPC()
    {
        step = rotateSpeed * Time.deltaTime;
        globe.transform.Rotate(0, step, 0);
    }

    //[PunRPC]
    void RotateRightRPC()
    {
        step = rotateSpeed * Time.deltaTime;
        globe.transform.Rotate(0, -step, 0);
    }
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

    void SelectNextYear()
    {

        selectedYear = Mathf.Min(selectedYear + 1, 2018);
        //photonView.RPC("UpdateSelectedYearRPC", RpcTarget.All, selectedYear);
        UpdateSelectedYearRPC(selectedYear);
    }

    void SelectPreviousYear()
    {
        selectedYear = Mathf.Max(selectedYear - 1, 1991);
        //photonView.RPC("UpdateSelectedYearRPC", RpcTarget.All, selectedYear);
        UpdateSelectedYearRPC(selectedYear);
    }

   
    void UpdateSelectedYearRPC(int year)
    {
        selectedYear = year;
        Debug.Log("Selected Year: " + selectedYear);
        colorizeScript.ColorizeCountries(selectedYear);
    }
    //**Change Year Code End**

    //**Interaction and Ownership handling**
    
    //**Interaction and Ownership handling End**
}