using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using WPM;

public class GlobeInteraction : MonoBehaviourPunCallbacks
{
    private bool isInteracting = false;
    public float rotateSpeed = 100f;
    private float step;
    private int selectedYear;
    public ColorizeCountriesScript colorizeScript;
    public DataManager dataManager;
    //public WorldMapGlobe map;

    void Start()
    {
        selectedYear = 2018;
        //map = WorldMapGlobe.instance;
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
            //photonView.RPC("RotateLeftRPC", RpcTarget.All);
            RotateLeftRPC();
            //Debug.

        }

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight) || Input.GetKeyDown(KeyCode.M))
        {
            //photonView.RPC("RotateRightRPC", RpcTarget.All);
            RotateRightRPC();


        }

        if (OVRInput.Get(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.K))
        {
            SelectNextYear();
        }
        if (OVRInput.Get(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.J))
        {
            SelectPreviousYear();
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
        transform.Rotate(0, step, 0);
    }

    //[PunRPC]
    void RotateRightRPC()
    {
        step = rotateSpeed * Time.deltaTime;
        transform.Rotate(0, -step, 0);
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