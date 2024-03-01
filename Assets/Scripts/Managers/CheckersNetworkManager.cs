using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckersNetworkManager : NetworkManager
{
    [SerializeField] GameObject gameOverHandlerPrefab, boardPrefab, 
        turnsHandlerPrefab;

    public static event Action ClientOnConnected;
    public List<PlayerNetwork> NetworkPlayeers { get; } = new List<PlayerNetwork>();

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

        player.LobyOwner = player.IsWhite = numPlayers == 1;
        player.DisplayName = player.IsWhite ? "Светлый" : "Темный";
    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        PlayerNetwork player = conn.identity.GetComponent<PlayerNetwork>();
        NetworkPlayeers.Remove(player);
        base.OnServerDisconnect(conn);
    }
    public override void OnStopServer()
    {
        NetworkPlayeers.Clear();
    }

}
