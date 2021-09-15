using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // Singleton instance
    public static RoomManager Instance;

    private void Awake()
    {
        // Check if instance already exists
        if (Instance != null)
        {
            // Destroy current game object
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void OnEnable()
    {
        base.OnEnable();

        // Add OnSceneLoaded method to list of delegates for sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        base.OnDisable();

        // Add OnSceneLoaded method to list of delegates for sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        // Check if scene is game scene
        if (scene.buildIndex == 1)
        {
            // Instantiate player manager
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }
}
