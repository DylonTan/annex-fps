using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text playerListItemText;

    private Player player;

    public void Init(Player _player)
    {
        player = _player;
        // Set player list item text to player's nickname
        playerListItemText.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // Check if this player matches leaving player and destroy current game object
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        // Destroy current game object
        Destroy(gameObject);
    }
}
