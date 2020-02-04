#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Book : MonoBehaviour
{
    #region public DATA
    public List<Transform> RotatePoints = new List<Transform>();
    #endregion

    #region private DATA
    bool Open = false;
    public int HandelsActive = 0;
    #endregion

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HandelsActive == 0)
        {
            Open = false;
        }
        else
        {
            Open = true;
        }

        for (int i = 0; i< RotatePoints.Count; i++)
        {
            Transform RotatePoint = RotatePoints[i];
            
            if (Open)
            {
                if (RotatePoint.localRotation.eulerAngles.y < 175)
                {
                    RotatePoint.localRotation = Quaternion.Euler(0, RotatePoint.localRotation.eulerAngles.y + 125 * Time.deltaTime, 0);
                }

                if (RotatePoint.localRotation.eulerAngles.y > 175)
                {
                    RotatePoint.localRotation = Quaternion.Euler(0, 175, 0);
                }
            }
            else
            {
                if (RotatePoint.localRotation.eulerAngles.y > 0)
                {
                    RotatePoint.localRotation = Quaternion.Euler(0, RotatePoint.localRotation.eulerAngles.y - 125 * Time.deltaTime, 0);
                }

                if (RotatePoint.localRotation.eulerAngles.y > 200)
                {
                    RotatePoint.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
    }
}
