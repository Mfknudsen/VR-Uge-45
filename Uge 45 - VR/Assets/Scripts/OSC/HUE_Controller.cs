#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class HUE_Controller : MonoBehaviour
{
    #region public DATA
    [Header("OSC Settings:")]
    public OSC OSC;  //The OSC script that is used to send, receive and setup the osc functions in this script.
    [Tooltip("Address for input and output")]
    public string inAddress = "/", outAddress = "/";
    #endregion

    #region private DATA
    bool CanSendOSC = false;
    #endregion

    void Start()
    {
        OSC.SetAddressHandler(inAddress, ReceiveInfoFromHUE);  //Use the functions ReceiveInfoFromHUE when the message contains the equel to inAddress.
    }

    public void ReceiveInfoScript(Vector3 RGB)  //Get the RGB value to send to HUE.
    {
        //Making sure the values are usable.
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

    void SendInfoToHUE(Vector3 RGB)  //Sending the RGB values as three float values in an osc message.
    {
        if (CanSendOSC == true && RGB != null)
        {
            OscMessage message = new OscMessage();  //Making a new osc message-
            message.address = outAddress;
            message.values.Add(RGB.x);
            message.values.Add(RGB.y);
            message.values.Add(RGB.z);

            OSC.Send(message);  //Sending the osc message through the OSC script.
        }
    }

    void ReceiveInfoFromHUE(OscMessage message)  //Get and print the returned value from HUE.
    {
        string toPrint = "";
        for (int i = 0; i < message.values.Count; i++)
        {
            toPrint = toPrint + "Value[" + i + 1 + "]: " + message.values[i] + ". ";
        }

        if (toPrint != "")
        {
            Debug.Log(toPrint);
        }
    }
}
