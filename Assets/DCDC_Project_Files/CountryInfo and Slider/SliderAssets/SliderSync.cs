using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using Photon.Pun;

public class SliderSyncDirect : MonoBehaviourPunCallbacks, IPunObservable
{
    public Slider yearSlider; 
    public TextMeshProUGUI yearText;
    public OVRInput.Controller controller;

    void Start()
    {
        //yearSlider.value = 0.5f;
    }

    
    void Update()
    {
        if (yearSlider != null)
        {
            // Setting the Slider's minimum and maximum values
            yearSlider.minValue = 1990;
            yearSlider.maxValue = 2018;
            yearSlider.onValueChanged.AddListener(delegate { UpdateYearText(); });
        }

        // Initialise year text
        UpdateYearText();
    }

    
    void UpdateYearText()
    {
        int year = (int)yearSlider.value;
        yearText.text = year.ToString();
        //put in logic for year change/color changes/also for updated ui values f?
    }

    public void OnValueChanged()
    {
        //if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger, controller))
        //float currentValue = yearSlider.value;
        //float triggerValue = OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger, controller);
        //float sliderValue = Mathf.Lerp(yearSlider.minValue, yearSlider.maxValue, triggerValue);
        //yearSlider.value = sliderValue;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Local player sends data
            stream.SendNext(yearSlider.value);
        }
        else
        {
            // Remote player receives data
            yearSlider.value = (float)stream.ReceiveNext();
            UpdateYearText();
        }
    }

}
