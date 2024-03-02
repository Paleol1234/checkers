using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : Player
{
    public static event Action ClientOnInfoUpdated;
    public static event Action<bool> AuthorityOnLobbyOwnerStateUpdated;
    //Поля и свойства Start 


    [SyncVar(hook =nameof(ClientHindleDisplayNameUpdated))]
    private string displayName;
    public string DisplayName
    {
        get
        {
            return displayName;
        }
        [Server]
        set
        {
            if(value == "")
            {
                value = "default name";
            }
            displayName = value;
        }
    }

    [SyncVar(hook = nameof(AuthorityHandleLobbyOwnerStateUpdated))]
    private bool lobyOwner;
    public bool LobyOwner
    {
        get
        {
            return lobyOwner;
        }
        [Server]
        set
        {
            lobyOwner = value;
        }
    }
    //Поля и свойства Finish
    void ClientHindleDisplayNameUpdated(string oldName, string NewName)
    {
        ClientOnInfoUpdated?.Invoke();
    }
    public override void OnStartClient()
    {
        if (!isClientOnly)
        {
            return;
        }
        ((CheckersNetworkManager)NetworkManager.singleton).NetworkPlayeers.Add(this);
    }
    public override void OnStopClient()
    {
        if (!isClientOnly)
        {
            ((CheckersNetworkManager)NetworkManager.singleton).NetworkPlayeers.Remove(this);
        }
        ClientOnInfoUpdated?.Invoke();
    }
    public void AuthorityHandleLobbyOwnerStateUpdated(bool oldState, bool newState)
    {
        if (!hasAuthority)
        {
            return;
        }
        AuthorityOnLobbyOwnerStateUpdated?.Invoke(newState);
    }


}

