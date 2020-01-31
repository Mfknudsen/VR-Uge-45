using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Unpause : MonoBehaviour
{
    #region public DATA
    public GameObject Ingame;
    public GameObject Pause;
    #endregion

    #region private DATA
    #endregion

    void Start()
    {
        Ingame.SetActive(true);
        Pause.SetActive(false);
    }

    void Update()
    {
        if (Ingame.active == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Ingame.SetActive(false);
            Pause.SetActive(true);
        }
        else if (Ingame.active == false && Input.GetKeyDown(KeyCode.Escape))
        {
            Ingame.SetActive(true);
            Pause.SetActive(false);
            Ingame.transform.GetChild(2).GetComponent<PlayerHelpText>().ConteniuAnimtaion();
        }
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
