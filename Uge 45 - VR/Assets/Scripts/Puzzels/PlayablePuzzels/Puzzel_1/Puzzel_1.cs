#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

//Inhering from the script "Puzzel".
public class Puzzel_1 : Puzzel
{
    //All public data. Can be accesed from any other script and changed from the inspector.
    #region public DATA
    [Header("Required Input")]  //The input that is required for the script to function.
    public Door D_Complete;  //Getting the final door to open when this puzzel is complete.
    public Door D_Stage2, D_Stage3, D_Stage4;  //Getting the rest of the door. Seperat from "D_complete" because of how it would look in the inspector.
    public Buttom B_Stage1;  //Getting the buttom for stage 1 of the puzzel.
    public PressurePlate P_Stage2;  //Getting the pressure plate for stage 2 of the puzzel. 
    public Keyhole K_Stage3;  //Getting the keyhole for stage 3 of the puzzel.
    public Lever L_Stage4;  //Getting the lever for stage 4 of the puzzel.
    #endregion

    //All private data. Can only be affect by this script. Can only be seen in the inspector if [SerializeField] is used.
    #region private DATA
    bool stage_1_Complete = false, stage_2_Complete = false, stage_3_Complete = false, stage_4_Complete = false;  //Setting up bool to see when the different stages of the puzzel is complete.
    bool puzzelActive = false;  //Bool to seen when the hole puzzel is complete.
    #endregion

    void Update()
    {
        if (puzzelActive == false)  //If the puzzel is not complete then run through the stages of the puzzel.
        {
            if (stage_1_Complete == false)  //If the first stage is not complete then check if it can be completed.
            {
                if (B_Stage1.active == true)  //See if the buttom of stage one has been activated.
                {
                    OpenDoorByActive(true, D_Stage2);  //Open the door of stage two so stage two can begin.
                    stage_1_Complete = true;  //Stage one is now complete.
                }
            }
            else  //If the first stage is complete then check the second, third and fouth stages.
            {
                if (stage_2_Complete == false)  //If the second stage is not complete then check if it can be completed.
                {
                    if (P_Stage2.active == true)  //See if the pressure plate of stage two has been activated.
                    {
                        OpenDoorByActive(true, D_Stage3);  //Open the door of stage three so stage three can begin.
                        stage_2_Complete = true;  //Stage two is now complete.
                    }
                }
                else  //If the second stage is complete then check the third and fouth stages.
                {
                    if (stage_3_Complete == false)  //If the third stage is not complete then check if it can be completed.
                    {
                        if (K_Stage3.active == true)  //See if the keyhole has been activated.
                        {
                            OpenDoorByActive(true, D_Stage4);  //Open the door of stage four so stage four can begin.
                            stage_3_Complete = true;  //Stage three is now complete.
                        }
                    }
                    else  //If the third stage is complete then check the fouth stage.
                    {
                        if (stage_4_Complete == false)  //If the fouth stage is not complete then check if it can be completed.
                        {
                            if (L_Stage4.active == true)  //See if the lever has been activated.
                            {
                                OpenDoorByActive(true, D_Complete);  //Open the the final door to exit the room.
                                puzzelActive = true;  //The puzzel is now complete and it will no longer check the different stages.
                                Debug.Log("Puzzel: 1, Complete");
                            }
                        }
                    }
                }
            }
        }
    }
}
