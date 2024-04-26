using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawner : MonoBehaviour
{
    public GameObject localGameManagerPrefab, networkManagerPrefab, window;

    public void SpawnLocalGameManager()
    {
        Instantiate(localGameManagerPrefab);
    }

    public void SpawnNetworkManager()
    {
        Instantiate(networkManagerPrefab);
    }

}
