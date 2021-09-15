using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    private PhotonView pv;

    // Awake is called before Start
    private void Awake()
    {
        // Get reference to photon view
        pv = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Check if photon view is owned by local player
        if (pv.IsMine)
        {
            // Create player controller
            CreatePlayerController();
        }
    }

    void CreatePlayerController()
    {
        // Instantiate player controller prefab from resources folder
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
    }
}
