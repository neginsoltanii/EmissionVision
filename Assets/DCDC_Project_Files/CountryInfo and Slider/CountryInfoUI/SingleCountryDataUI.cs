using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Photon.Pun;
using Photon.Realtime;
using PhotonPun = Photon.Pun;
using PhotonRealtime = Photon.Realtime;


public class SingleCountryDataUI : MonoBehaviour
{
    public TMP_Text countryName;
    public TMP_Text co2ratio;


    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
    }


    [PunRPC]
    public void SetData(string country, string co2)
    {
        // Set UI data
        countryName.text = country;
        co2ratio.text = co2;
    }

    public void CloseThisCountry()
    {
        //Removes this object from the parent container
        // that has the Layout component
        gameObject.GetComponent<PhotonPun.PhotonView>().RPC("destroyObject", RpcTarget.All);
    }

    [PunRPC]
    public void destroyObject()
    {
        Destroy(gameObject);
    }
    



}
