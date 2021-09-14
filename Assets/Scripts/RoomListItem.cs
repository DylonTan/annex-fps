using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField]
    TMP_Text roomListItemText;

    RoomInfo roomInfo;

    public void Init(RoomInfo _roomInfo)
    {
        roomInfo = _roomInfo;
        roomListItemText.text = _roomInfo.Name;
    }

    public void onClick()
    {
        Launcher.Instance.JoinRoom(roomInfo);
    }
}
