#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Puzzel : MonoBehaviour
{
    public bool CheckAllActive(List<bool> actives)
    {
        for (int i = 0; i < actives.Count; i++)
        {
            if (actives[i] != true)
            {
                return false;
            }
        }

        return true;
    }

    public bool CheckAllKeysActive(List<GameObject> keyholes)
    {
        int t = 0;
        List<Keyhole> k = new List<Keyhole>();

        for (int j = 0; j < keyholes.Count; j++)
        {
            if (keyholes[j].GetComponent<Lever>() != null)
            {
                k.Add(keyholes[j].GetComponent<Keyhole>());
            }
        }

        for (int i = 0; i < keyholes.Count; i++)
        {
            if (k[i].checkCorrectKey() == true)
            {
                t++;
            }
            else
            {
                return false;
            }
        }
        if (t == k.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckAllPressurePlatesActive(List<GameObject> pressurePlates)
    {
        int t = 0;
        List<PressurePlate> p = new List<PressurePlate>();

        for (int j = 0; j < pressurePlates.Count; j++)
        {
            if (pressurePlates[j].GetComponent<Lever>() != null)
            {
                p.Add(pressurePlates[j].GetComponent<PressurePlate>());
            }
        }

        for (int i = 0; i < p.Count; i++)
        {
            if (p[i].active == true)
            {
                t++;
            }
            else
            {
                return false;
            }
        }
        if (t == p.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckAllLeverActive(List<GameObject> levers)
    {
        int t = 0;
        List<Lever> l = new List<Lever>();

        for (int j = 0; j < levers.Count; j++)
        {
            if (levers[j].GetComponent<Lever>() != null)
            {
                l.Add(levers[j].GetComponent<Lever>());
            }
        }

        for (int i = 0; i < levers.Count; i++)
        {
            if (l[i].active == true)
            {
                t++;
            }
            else
            {
                return false;
            }
        }
        if (t == l.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float CheckAllLeverProgress(List<GameObject> levers)
    {
        float tempProgress = 0;
        List<Lever> l = new List<Lever>();

        for (int j = 0; j < levers.Count; j++)
        {
            if (levers[j].GetComponent<Lever>() != null)
            {
                l.Add(levers[j].GetComponent<Lever>());
            }
        }


        for (int i = 0; i < l.Count; i++)
        {
            tempProgress += l[i].CountProcent();
        }

        tempProgress = Mathf.Ceil(tempProgress / l.Count);

        return tempProgress;
    }

    public void OpenDoorByActive(bool active, GameObject door)
    {
        if (door.GetComponent<Door>() != null)
        {
            Door d = door.GetComponent<Door>();

            if (active == true)
            {
                d.active = true;
            }
            else
            {
                d.active = false;
            }

            d.SwitchOpenClosed();
        }
        else
        {
            Debug.Log("Missing Component: Door");
        }
    }

    public void OpenDoorByProcent(float progress, GameObject door)
    {
        if (door.GetComponent<Door>() != null)
        {
            Door d = door.GetComponent<Door>();
            Vector3 t = d.ClosedTransform + ((d.OpenTransform - d.ClosedTransform) / 100 * progress);

            door.transform.position = t;
        }
        else
        {
            Debug.Log("Missing Component: Door");
        }
    }

    public void MoveCraneByProcent(float progressX, float progressY, float progressZ, GameObject crane)
    {
        if (crane.GetComponent<Crane>() != null)
        {
            Crane c = crane.GetComponent<Crane>();

            c.progressX = progressX;
            c.progressY = progressY;
            c.progressZ = progressZ;
        }
        else
        {
            Debug.Log("Missing Component: Crane");
        }
    }

    public void SwitchCraneClawByActive(bool active, GameObject crane)
    {
        if (crane.GetComponent<Crane>() != null)
        {
            Crane c = crane.GetComponent<Crane>();
            if (active == true)
            {
                c.LockClaw();
            }
            else
            {
                c.ReleaseClaw();
            }
        }
        else
        {
            Debug.Log("Missing Component: Crane");
        }
    }
}
