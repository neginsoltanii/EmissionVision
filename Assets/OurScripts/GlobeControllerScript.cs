using Photon.Pun;
using UnityEngine;
using WPM;
using PhotonPun = Photon.Pun;
using PhotonRealtime = Photon.Realtime;

public class GlobeControllerScript : MonoBehaviour
{
    public float rotateSpeed = 100f;
    private float step;
    private float rotationY;
    public float thumbstickThreshold = 0.1f;  // Threshold for thumbstick usage
    public int selectedYear;
    public ColorizeCountriesScript colorizeScript;

    public GameObject globe;
    private int globeID;
    public GameObject globePrefab;

    public GameObject slider;
    private int sliderID;
    public GameObject sliderPrefab;

    public GameObject UI;
    private int UIID;
    public GameObject UIPrefab;

    void Start()
    {
        selectedYear = 2018;
    }

    void Update()
    {
        // Check if the Y button on the controller is pressed to spawn the globe and slider
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("Y button pressed on controller");
            SpawnGlobe();
            SpawnSlider();
        }

        // Get thumbstick input for rotation
        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        // Check if thumbstick is being used or 'N' key is pressed
        if (thumbstickInput.magnitude > thumbstickThreshold || Input.GetKeyDown(KeyCode.N))
        {
            rotationY = thumbstickInput.x;
            RotateRPC(rotationY);
        }
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
        Debug.Log("Instantiated Globe");
        globe = networkedGlobe.gameObject;

        var photonGrabbable = networkedGlobe.GetComponent<PhotonGrabbableObject>();
        globeID = networkedGlobe.GetComponent<PhotonPun.PhotonView>().ViewID;

        if (photonGrabbable == null)
        {
            Debug.LogError("PhotonGrabbableObject component is missing on the instantiated globe.");
            return;
        }

        photonGrabbable.TransferOwnershipToLocalPlayer();
        Debug.Log("Ownership transferred to local player.");
    }

    private void SpawnSlider()
    {
        Debug.Log("Spawning Slider");
        if (sliderPrefab == null)
        {
            Debug.LogError("Slider prefab is not assigned.");
            return;
        }

        var networkedSlider = PhotonNetwork.Instantiate(sliderPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log("Instantiated Slider");
        slider = networkedSlider.gameObject;

        var photonGrabbable = networkedSlider.GetComponent<PhotonGrabbableObject>();
        sliderID = networkedSlider.GetComponent<PhotonPun.PhotonView>().ViewID;

        if (photonGrabbable == null)
        {
            Debug.LogError("PhotonGrabbableObject component is missing on the instantiated slider.");
            return;
        }

        photonGrabbable.TransferOwnershipToLocalPlayer();
        Debug.Log("Ownership transferred to local player.");
    }

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

        Debug.Log("Updating year to: " + year);
        colorizeScript.ColorizeCountries(year, globe);
    }
}