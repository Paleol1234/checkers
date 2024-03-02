using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] Button startGameButton;
    [SerializeField] Text[] playerNameTexts = new Text[2];

    public void StartGame()
    {
        
    }
    void ClientHandleInfoUpdated()
    {
        List<PlayerNetwork> Players = ((CheckersNetworkManager)NetworkManager.singleton).NetworkPlayeers;
        for(int i =0; i< Players.Count; i++)
        {
            playerNameTexts[i].text = Players[i].DisplayName;
        }
        for(int i = Players.Count; i<playerNameTexts.Length;i++)
        {
            playerNameTexts[i].text = "Ждем игрока";
        }
        startGameButton.interactable = Players.Count > 1;
    }
    private void Start()
    {
        PlayerNetwork.ClientOnInfoUpdated += ClientHandleInfoUpdated;
        PlayerNetwork.AuthorityOnLobbyOwnerStateUpdated += AuthorityHandleLoobyOwnerStateUpdated;
    }
    private void OnDestroy()
    {
        PlayerNetwork.ClientOnInfoUpdated -= ClientHandleInfoUpdated;
        PlayerNetwork.AuthorityOnLobbyOwnerStateUpdated -= AuthorityHandleLoobyOwnerStateUpdated;

    }
    void AuthorityHandleLoobyOwnerStateUpdated(bool state)
    {
        startGameButton.gameObject.SetActive(state);
    }
}
