using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;

    [Header("LobbyPanel")]
    public TextMeshProUGUI textStatus;
    private const int maxPlayerConstraint = 6;      // 방 최대인원 상수.
    public InputField roomInputField;


    private void Awake()
    {   
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);  // 이미 인스턴스가 존재할 경우 현재 인스턴스 파괴
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);  // 다른 장면으로 넘어가도 파괴되지 않도록 설정
        }
    }

    private void Start()
    {
        Connect();
    }

    private void Update() => textStatus.text = PhotonNetwork.NetworkClientState.ToString();

    //-----------------------------------------------------------------------------------------------------------------


    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        // NOTICE: 대규모 게임이 아니므로 단일로비 사용. 따라서 서버 연결 시 곧장 로비로 이동
        PhotonNetwork.JoinLobby();
    }



    //------------------------------------------------------------------------
    // 게임룸 호스팅
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = maxPlayerConstraint };
        PhotonNetwork.CreateRoom(roomInputField.text, roomOptions);
        //RoomInput.text == "" ? "Room" + Random.Range(0, 100) : roomInputField.text
    }

    public void JoinRoom()
    {
        //PhotonNetwork.JoinRoom(roomName);
        PhotonNetwork.JoinRoom(roomInputField.text);
    }
    public void JoinRandomRoom()
    {
        // 빠른참가를 위한 랜덤룸 입장
        PhotonNetwork.JoinRandomRoom();
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    public override void OnCreatedRoom()
    {
        Debug.Log("Room created successfully");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room creation failed: " + message);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room join failed: " + message);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError("Random room join failed: " + message);
    }
}

