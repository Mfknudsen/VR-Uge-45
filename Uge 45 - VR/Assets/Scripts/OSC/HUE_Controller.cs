#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class HUE_Controller : MonoBehaviour
{
    #region public DATA
    [Header("OSC Settings:")]
    public OSC OSC;
    [Tooltip("Visual Debuging From Inspector")]
    public string IP_Visual;
    [Tooltip("Visual Debuging From Inspector")]
    public int outPort_Visual, inPort_Visual;
    [Tooltip("Address for input and output")]
    public string inAddress = "/wek/output", outAddress = "/wek/inputs";
    #endregion

    #region private DATA
    bool CanSendOSC = false;
    #endregion

    void Start()
    {
        IP_Visual = OSC.outIP;
        outPort_Visual = OSC.outPort;
        inPort_Visual = OSC.inPort;

        OSC.SetAddressHandler(inAddress, ReceiveInfoFromHUE);
    }

    public void ReceiveInfoScript(Vector3 RGB)
    {
        if (RGB != null)
        {
            if (RGB.x < 0)
            {
                RGB.x = 0;
            }
            else if (RGB.x > 255)
            {
                RGB.x = 255;
            }

            if (RGB.y < 0)
            {
                RGB.y = 0;
            }
            else if (RGB.y > 255)
            {
                RGB.y = 255;
            }

            if (RGB.z < 0)
            {
                RGB.z = 0;
            }
            else if (RGB.z > 255)
            {
                RGB.z = 255;
            }

            SendInfoToHUE(RGB);
        }
    }

    void SendInfoToHUE(Vector3 RGB)
    {
        if (CanSendOSC == true && RGB != null)
        {
            OscMessage message = new OscMessage();
            message.address = outAddress;
            message.values.Add(RGB.x);
            message.values.Add(RGB.y);
            message.values.Add(RGB.z);

            OSC.Send(message);
        }
    }

    void ReceiveInfoFromHUE(OscMessage message)
    {
        string toPrint = "";
        for (int i = 0; i < message.values.Count; i++){
            toPrint = toPrint + "Value["+i+1+"]: "+ message.values[i]+". ";
        }

        if(toPrint != ""){
            Debug.Log(toPrint);
        }
    }
}
