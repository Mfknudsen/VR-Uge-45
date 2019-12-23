using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Buttom_Handel : MonoBehaviour
{
    public Buttom B;

    void OnDetachedFromHand()
    {
        if(B != null){
            B.SwitchActive(false);
        }
    }

    void OnAttachedToHand()
    {
        if(B != null){
            B.SwitchActive(true);
        }
    }
}