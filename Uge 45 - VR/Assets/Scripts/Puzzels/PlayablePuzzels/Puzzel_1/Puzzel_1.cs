#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Puzzel_1 : Puzzel
{
    #region public DATA
    #endregion

    #region private DATA
    DisplayScreen DS;
    #endregion
    
    void Start()
    {
        DS = transform.Find("DisplayScreen").GetComponent<DisplayScreen>();
    }
    void Update()
    {

    }
}
