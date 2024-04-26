using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class GameOverHandlerNetworked : GameOverHandler
{
    [ClientRpc]
    void RpcGameOver(string result)
    {
        CallGameOver(result);
        print("Победа в нутри RpcGameOver");

    }
    [Server]
    void HandleGameOver(string result)
    {
        RpcGameOver(result);
        print("Победа в нутри HandlegameOver");
    }
    public override void OnStartServer()
    {
        TurnsHandler.Instance.OnGameOver += HandleGameOver;
    }
    public override void OnStopServer()
    {
        TurnsHandler.Instance.OnGameOver -= HandleGameOver;

    }

}
