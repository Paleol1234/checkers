using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovementHandlerNetwork : PieceMovementHandler
{
    public static event Func<PiecePromotionHandler, int, int, bool> serverOnPieceReachedBackLine;
    protected override void ReachedBackline(Vector2Int newPosition)
    {
        serverOnPieceReachedBackLine?.Invoke(promotionHandler,newPosition.x,newPosition.y);
    }
    [Server]
    bool TryPromodePieceOnBoard(PiecePromotionHandler promotedPiece,int x,int y)
    {
        return true;
    }
    public override void OnStartAuthority()
    {
        TilesSelectionHandler.OnTileSelected += HandleTileSelected;
    }
    public override void OnStopClient()
    {
        TilesSelectionHandler.OnTileSelected -= HandleTileSelected;
    }
    protected override void Move(Vector3 position, bool nextTurn)
    {
        CMDMove(position,nextTurn);
    }
    [Command]
    void CMDMove(Vector3 position,bool nextTurn)
    {
        base.Move(position,nextTurn);
    }
    protected override void Capture(Vector2Int piecePosition)
    {
        CMDCapture(piecePosition);
    }
    [Command]
   void CMDCapture(Vector2Int piecePosition)
    {
        base.Capture(piecePosition);
    }
    protected override void PlayAudio()
    {
        RPCPlayAudio();
    }
    [ClientRpc]
    void RPCPlayAudio()
    {
        base.PlayAudio();
    }
}
