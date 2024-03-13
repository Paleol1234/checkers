using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiecesHandlerNetworked : PlayerPiecesHandler
{
    public override void OnStartServer()
    {
        CheckersNetworkManager.ServerOnGameStarted += HandleGameStarted;
    }
    public override void OnStopServer()
    {
        CheckersNetworkManager.ServerOnGameStarted -= HandleGameStarted;
    }
}
