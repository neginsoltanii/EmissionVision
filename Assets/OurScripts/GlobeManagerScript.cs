using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using WPM;
using PhotonPun = Photon.Pun;
using PhotonRealtime = Photon.Realtime;

public class GlobeManagerScript : MonoBehaviour
{
    

    public WorldMapGlobe worldMapGlobe;

    public static GlobeManagerScript instance;
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //worldMapGlobe = GetComponent<WorldMapGlobe>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnGlobe()
    {
        Debug.Log("Went to method");
        var networkedGlobe = PhotonPun.PhotonNetwork.Instantiate(worldMapGlobe.name,new Vector3(0,0,0), Quaternion.identity);
        //var photonGrabbable = networkedGlobe.GetComponent<PhotonGrabbableObject>();
        //photonGrabbable.TransferOwnershipToLocalPlayer();
    }
}
