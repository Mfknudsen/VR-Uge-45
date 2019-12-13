using System.Collections;
using UnityEngine;

public class TEST_OSC : MonoBehaviour
{
    #region public DATA
    public OSC osc;
    public string IP;
    public int outPort;
    public int inPort;

    #endregion

    #region private DATA
    private bool unlocked = false;
    private float unlockProgress = 0;

    #endregion

    void Start()
    {
        IP = osc.outIP;
        outPort = osc.outPort; //Standard: 6161
        inPort = osc.inPort;   //Standard: 6969

        osc.SetAddressHandler("/TestVector", ReceiveOSC); //Tell the OSC script to use the function "ReceiveOSC" if the message has the adress "/TestVector"

        StartCoroutine(ConstantDelayTest());
    }

    void SendOSC()
    {
        OscMessage message = new OscMessage(); //Making a new empty message
        message.address = "/wek/input"; //Giving the message an addres so the OSC script will know where to use the message after it is recieved
        message.values.Add(Random.Range(-10, 10)); //Adding a Float or Int values to the message
        message.values.Add(Random.Range(-10, 10)); //Adding a Float or Int values to the message
        message.values.Add(Random.Range(-10, 10)); //Adding a Float or Int values to the message
        osc.Send(message); //Sending the message to the outport
    }

    void ReceiveOSC(OscMessage message)
    {
        //"Float Value" = "message.GetFloat(index)"
        unlockProgress = message.GetFloat(0);
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
