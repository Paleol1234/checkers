using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceNetwork : NetworkBehaviour
{
    [SyncVar (hook = nameof(HandleOwnerSet))]
    PlayerPiecesHandler owner;


    public override void OnStartServer()
    {
        owner = connectionToClient.identity.GetComponent<PlayerPiecesHandler>();
        Board.Instance.OnPieceCaptured += ServerHandlePieceCapturet;
    }
    public override void OnStopServer()
    {
        Board.Instance.OnPieceCaptured -= ServerHandlePieceCapturet;
    }
    private void HandleOwnerSet(PlayerPiecesHandler oldOwner,PlayerPiecesHandler newOwner)
    {
        transform.parent = newOwner.PiecesParent;
    }
    [Server]
    void ServerHandlePieceCapturet(Vector3 capturetPiecePosition)
    {
        if(capturetPiecePosition != transform.position)
        {
            return;
        }
        NetworkServer.Destroy(gameObject);
    }
}
