using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Photon.PunBehaviour
{
    public static GameManager instance;
    public static GameObject localPlayer;
    GameObject defaultSpawnPoint;

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;

        defaultSpawnPoint = new GameObject("Default SpawnPoint");
        defaultSpawnPoint.transform.position = new Vector3(22, -22, 0);
        defaultSpawnPoint.transform.SetParent(transform, false);

        PhotonNetwork.automaticallySyncScene = true;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("Bomberman_Bm1");
    }

    public void JoinGame()
    {
        RoomOptions ro = new RoomOptions();
        ro.maxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Default Room", ro, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.LoadLevel("MPLevel1");
            }
    }

    void OnLevelWasLoaded(int levelNumber)
    {
        if (!PhotonNetwork.inRoom) return;
        var spawnPoint = GetRandomSpawnPoint();

        localPlayer = PhotonNetwork.Instantiate(
                        "Player",
                        spawnPoint.position,
                        spawnPoint.rotation, 0);
    }

    Transform GetRandomSpawnPoint()
    {
        var spawnPoints = GetAllObjectsOfTypeInScene<SpawnPoint>();
        if (spawnPoints.Count == 0)
        {
            return defaultSpawnPoint.transform;
        }
        else
        {
            return spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
        }
    }

    public static List<GameObject> GetAllObjectsOfTypeInScene<T>()
    {
        List<GameObject> objectsInScene = new List<GameObject>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject))
                 as GameObject[])
        {
            if (go.hideFlags == HideFlags.NotEditable ||
                go.hideFlags == HideFlags.HideAndDontSave)
                continue;

            if (go.GetComponent<T>() != null)
                objectsInScene.Add(go);
        }

        return objectsInScene;
    }

}