#region Systems
using System.Collections;
using UnityEngine;
#endregion 

public class TEST_OSC : MonoBehaviour
{
    #region public DATA
    [Header("OSC Settings")]
    public OSC osc;
    public string IP;    //For looks
    public int outPort;  //For looks
    public int inPort;   //For looks
    public string inAddress = "/wek/outputs";
    public string outAddress = "/wek/inputs";
    #endregion

    #region private DATA
    private bool unlocked = false;
    private float unlockProgress = 0; //Up to 100%
    #endregion

    void Start()
    {
        IP = osc.outIP;
        outPort = osc.outPort; //Standard: 6161
        inPort = osc.inPort;   //Standard: 6969

        osc.SetAddressHandler(inAddress, ReceiveOSC); //Tell the OSC script to use the function "ReceiveOSC" if the message has the adress "inAddress"
    }

    void SendOSC()
    {
        OscMessage message = new OscMessage(); //Making a new empty message
        message.address = outAddress;          //Giving the message an addres so the OSC script will know where to use the message after it is recieved. Example: "/wek/inputs"
        message.values.Add(1);                 //Adding a Float or Int values to the message
        osc.Send(message);                     //Sending the message to the outport
    }

    void ReceiveOSC(OscMessage message)
    {
        //"Float Value" = "message.GetFloat(index)"
        /*if(message.GetFloat(0) == 1){
            unlocked = true;
            unlockProgress = 100;
        } else {
            unlocked = false;
        }*/

        transform.position = new Vector3(message.GetFloat(0), message.GetFloat(1), message.GetFloat(2));
    }
}