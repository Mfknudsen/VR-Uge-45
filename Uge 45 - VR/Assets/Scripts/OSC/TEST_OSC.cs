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

    //IEnumerators is functions that can be delayed using realtime seconds without stopping the next code from running.
    //void Start(){
    //  IEnumerator ConstantDelayTest()
    //  {
    //      yield return new WaitForSeconds(5);
    //      SendOSC();
    //  }
    //
    //  float x = 1;
    //}
    //
    //While the IEnumerator function may be placed before the x value will be set as 1, the code where x is being set will still execute before the "SendOSC" function inside the IEnumerator due to it being delayed by 5 seconds before executing the rest for the IEnumerator function.
    IEnumerator ConstantDelayTest()
    {
        yield return new WaitForSeconds(5);
        SendOSC();
    }
}