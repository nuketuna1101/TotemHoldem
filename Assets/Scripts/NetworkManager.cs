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
    private const int maxPlayerConstraint = 6;      // �� �ִ��ο� ���.
    public InputField roomInputField;


    private void Awake()
    {   
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);  // �̹� �ν��Ͻ��� ������ ��� ���� �ν��Ͻ� �ı�
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);  // �ٸ� ������� �Ѿ�� �ı����� �ʵ��� ����
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
        // NOTICE: ��Ը� ������ �ƴϹǷ� ���Ϸκ� ���. ���� ���� ���� �� ���� �κ�� �̵�
        PhotonNetwork.JoinLobby();
    }



    //------------------------------------------------------------------------
    // ���ӷ� ȣ����
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
        // ���������� ���� ������ ����
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

