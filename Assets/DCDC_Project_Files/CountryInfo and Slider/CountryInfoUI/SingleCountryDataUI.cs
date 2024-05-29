using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using PhotonPun = Photon.Pun;
using PhotonRealtime = Photon.Realtime;

public class SingleCountryDataUI : MonoBehaviourPunCallbacks
{
    public TMP_Text countryName;
    public TMP_Text co2ratio;

    void Start()
    {
        // Ensure PhotonView is properly initialized and assigned a valid ViewID
        if (photonView == null)
        {
            PhotonView pv = gameObject.AddComponent<PhotonView>();
            pv.ViewID = PhotonNetwork.AllocateViewID(false);
        }
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
        if (photonView.IsMine)
        {
            photonView.RPC("DestroyObject", RpcTarget.AllBuffered);
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
            photonView.RPC("DestroyObject", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void DestroyObject()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
