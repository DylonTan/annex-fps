using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Singleton instance
    public static Launcher Instance;

    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private Transform roomList;
    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private Transform playerList;
    [SerializeField] private GameObject playerListItemPrefab;
    [SerializeField] private GameObject startGameButton;

    // Awake is called before Start
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Connect to master server with preset settings
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        // Join lobby in master server
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        // Open title menu
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
        // Set player nickname
        PhotonNetwork.NickName = "Player " + Random.Range(1, 100);
    }

    public override void OnJoinedRoom()
    {
        // Set current room name
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        // Open room menu
        MenuManager.Instance.OpenMenu("room");

        foreach (Transform trans in playerList)
        {
            Destroy(trans.gameObject);
        }
        
        // Get list of players in current room
        Player[] players = PhotonNetwork.PlayerList;

        // Loop through each player and create a player list item
        foreach (Player player in players)
        {
            Instantiate(playerListItemPrefab, playerList).GetComponent<PlayerListItem>().Init(player);
        }

        // Set start game button to active for master client
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        // Set start game button to active for master client
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // Set error text message
        errorText.text = "Room Creation Failed: " + message;

        // Open error menu
        MenuManager.Instance.OpenMenu("error");
    }

    public override void OnLeftRoom()
    {
        // Open title menu
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> _roomInfoList)
    {
        // Clear the list
        foreach (Transform trans in roomList)
        {
            Destroy(trans.gameObject);
        }

        // Loop through each room info and create a room list item
        foreach (RoomInfo roomInfo in _roomInfoList)
        {
            if (roomInfo.RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomList).GetComponent<RoomListItem>().Init(roomInfo);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Create new player list item
        Instantiate(playerListItemPrefab, playerList).GetComponent<PlayerListItem>().Init(newPlayer);
    }

    public void CreateRoom()
    {
        // Return if room name is null
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        // Create new room
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        // Open loading menu
        MenuManager.Instance.OpenMenu("loading");
    }

    public void LeaveRoom()
    {
        // Leave current room
        PhotonNetwork.LeaveRoom();
        // Open loading menu
        MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo _roomInfo)
    {
        // Join room with provided name
        PhotonNetwork.JoinRoom(_roomInfo.Name);
        // Open loading menu
        MenuManager.Instance.OpenMenu("loading");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
