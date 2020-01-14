using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRANE_TEST : Puzzel
{
    public List<Lever> Levers = new List<Lever>();
    public Buttom Buttom;
    public Crane Crane;

    void Update()
    {
        MoveCraneByProcent(Levers[0].progressInProcent, Levers[1].progressInProcent, Levers[2].progressInProcent, Crane);

        SwitchCraneClawByActive(Buttom, Crane);
    }
}
