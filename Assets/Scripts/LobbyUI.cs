using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 로비 UI 관리 스크립트
public class LobbyUI : MonoBehaviour
{
    public InputField roomNameInput;
    public Button createRoomButton;
    public Button joinRoomButton;

    private void Start()
    {
        createRoomButton.onClick.AddListener(() => CreateRoom());
        joinRoomButton.onClick.AddListener(() => JoinRoom());
    }

    private void CreateRoom()
    {
        //NetworkManager.Instance.CreateRoom(roomNameInput.text);
        try
        {
            NetworkManager.Instance.CreateRoom();
        }
        catch (Exception e)
        {
            Debug.Log("An error occurred: " + e.Message);
        }
    }

    private void JoinRoom()
    {
        //NetworkManager.Instance.JoinRoom(roomNameInput.text);
        try
        {
            NetworkManager.Instance.JoinRoom();
        }
        catch (Exception e)
        {
            Debug.Log("An error occurred: " + e.Message);
        }
    }
}

