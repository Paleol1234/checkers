using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckersNetworkManager : NetworkManager
{
    //Поля
    [SerializeField] GameObject gameOverHandlerPrefab, boardPrefab, 
        turnsHandlerPrefab;
    //События
    public static event Action ClientOnConnected;
    public static event Action ServerOnGameStarted;
    //свойства
    public List<PlayerNetwork> NetworkPlayeers { get; } = new List<PlayerNetwork>();
    public List<Player> Players { get; } = new List<Player>();
    //методы
    public override void OnStartServer()
    {
        GameObject boardInstance = Instantiate(boardPrefab);
        NetworkServer.Spawn(boardInstance);
        GameObject turnsHandlerInstance = Instantiate(turnsHandlerPrefab);
        NetworkServer.Spawn(turnsHandlerInstance);
    }
    
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        ClientOnConnected?.Invoke();
    }
    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        SceneManager.LoadScene("Lobby Scene");
        Destroy(gameObject);
    }
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        GameObject playerInstance = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, playerInstance);
        PlayerNetwork player = playerInstance.GetComponent<PlayerNetwork>();
        NetworkPlayeers.Add(player);
        Players.Add(player);

        player.LobyOwner = player.IsWhite = numPlayers == 1;
        player.DisplayName = player.IsWhite ? "Светлый" : "Темный";

    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        PlayerNetwork player = conn.identity.GetComponent<PlayerNetwork>();
        NetworkPlayeers.Remove(player);
        Players.Remove(player);
        base.OnServerDisconnect(conn);
    }
    public override void OnStopServer()
    {
        NetworkPlayeers.Clear();
        Players.Clear();
    }
    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName.StartsWith("Game"))
        {
            GameObject gameOverhandleInstance = Instantiate(turnsHandlerPrefab);
            NetworkServer.Spawn(gameOverhandleInstance);
            ServerOnGameStarted?.Invoke();
        }

    }

}
