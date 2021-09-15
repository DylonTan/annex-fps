using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text roomListItemText;

    public RoomInfo roomInfo;

    public void Init(RoomInfo _roomInfo)
    {
        roomInfo = _roomInfo;

        // Set room list item text to current room's name
        roomListItemText.text = _roomInfo.Name;
    }

    public void onClick()
    {
        // Join room with current room info
        Launcher.Instance.JoinRoom(roomInfo);
    }
}
