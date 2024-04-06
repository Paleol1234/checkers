using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardNetwork : Board
{
    public override event Action<Vector3> OnPieceCaptured;
    readonly SyncList<int[]> boardList = new SyncList<int[]>();
    public override IList<int[]> BoardList {get {return boardList;} }
    public override void OnStartServer()
    {
        FillBoardList(boardList);
    }
    [Server]
    public override void MoveOnBoard(Vector2Int oldPosition, Vector2Int newPosition, bool nextTurn)
    {
        base.MoveOnBoard(boardList,oldPosition,newPosition);
        RPCmoveOnBoard(oldPosition,newPosition,nextTurn);
    }
    [ClientRpc]
    void RPCmoveOnBoard(Vector2Int oldPosition, Vector2Int newPostion, bool nexTurn)
    {
        if (NetworkServer.active)
        {
            return;
        }
        MoveOnBoard(boardList, oldPosition, newPostion);
        if (nexTurn)
        {
            NetworkClient.connection.identity.GetComponent<PlayerNetwork>().CmdNextTurn();
        }
    }
    [Server]
    public override void CaptureOnBoard(Vector2Int piecePosition)
    {
        Capture(boardList, piecePosition);
        RpcCaptureOnBoard(piecePosition);
        OnPieceCaptured?.Invoke(
            new Vector3(piecePosition.x,0,piecePosition.y));
    }
    [ClientRpc]
    void RpcCaptureOnBoard(Vector2Int piecePostion)
    {
        Capture(boardList,piecePostion);
    }
    [Server]
    bool TryPromotePieceOnBoard(PiecePromotionHandler promotedPiece,int x, int z)
    {
        PromotePieceOnBoard(boardList, x, z);
        RpcPromotePieceOnBoard(x, z);
        return true;
    }
    [ClientRpc]
    void RpcPromotePieceOnBoard(int x, int z)
    {
        if (NetworkServer.active)
        {
            return;
        }
        PromotePieceOnBoard(boardList, x, z);
    }
}
