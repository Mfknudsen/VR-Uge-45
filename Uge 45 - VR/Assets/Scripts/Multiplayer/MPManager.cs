#region Systems
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
#endregion

public class MPManager : MonoBehaviourPunCallbacks
{
    #region public DATA
    public GameObject[] VRObjectsOnConnect;
    public GameObject[] InstructorObjectsOnConnect;
    public GameObject VR_UI;
    public GameObject Camera;
    public string Player;
    #endregion

    #region private DATA
    #endregion

    void Start()
    {
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Join();
    }

    public void Join()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public void CreateRoom()
    {
        RoomOptions Options = new RoomOptions { MaxPlayers = 10, IsOpen = true, IsVisible = true };
        PhotonNetwork.CreateRoom("defaultFreeForAll", Options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Player has joined a room.");
        Debug.Log("There is currently " + PhotonNetwork.CountOfPlayers + " in this room.");

        GivePlayerClass();
    }

    public void GivePlayerClass()
    {
        if (PhotonNetwork.CountOfPlayers == 1)
        {
            Player = "VR";
        }
        else
        {
            Player = "Instructor";
        }

        if (Player == "VR")
        {
            foreach (GameObject obj in VRObjectsOnConnect)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in InstructorObjectsOnConnect)
            {
                obj.SetActive(true);
            }
        }
        else if (Player == "Instructor")
        {
            foreach (GameObject obj in InstructorObjectsOnConnect)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in VRObjectsOnConnect)
            {
                obj.SetActive(true);
            }

            Camera.SetActive(true);
        }
    }
}
