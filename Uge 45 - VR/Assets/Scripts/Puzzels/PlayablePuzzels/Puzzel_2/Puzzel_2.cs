#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Puzzel_2 : Puzzel
{
    #region public DATA
    public Door D_Start;
    #endregion

    #region private DATA
    #endregion
    
    void Start(){
        OpenDoorByActive(true, D_Start);
    }

    void Update(){

    }
}
