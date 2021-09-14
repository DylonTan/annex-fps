using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField]
    TMP_InputField roomNameInputField;
    [SerializeField]
    TMP_Text errorText;
    [SerializeField]
    TMP_Text roomNameText;
    [SerializeField]
    Transform roomList;
    [SerializeField]
    GameObject roomListItemPrefab;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
    }

    public override void OnJoinedRoom()
    {
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        MenuManager.Instance.OpenMenu("room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> _roomInfoList)
    {
        // Clear the list
        foreach (Transform trans in roomList)
        {
            Destroy(trans.gameObject);
        }

        // Populate the list
        foreach (RoomInfo roomInfo in _roomInfoList)
        {
            Instantiate(roomListItemPrefab, roomList).GetComponent<RoomListItem>().Init(roomInfo);
        }
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo _roomInfo)
    {
        PhotonNetwork.JoinRoom(_roomInfo.Name);
        MenuManager.Instance.OpenMenu("loading");
    }
}
