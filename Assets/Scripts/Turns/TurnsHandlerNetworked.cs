using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsHandlerNetworked : TurnsHandler
{
    protected override void FillMovesList()
    {
        base.FillMovesList();
        RPCGenarateMoves(piecesHandler);
    }
    [ClientRpc]
    void RPCGenarateMoves(PlayerPiecesHandler playerPieces)
    {
        if (NetworkServer.active)
        {
            return;
        }
        GenerateMoves(playerPieces.PiecesParent);
    }
}

