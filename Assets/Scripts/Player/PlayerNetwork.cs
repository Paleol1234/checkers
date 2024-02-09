using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : Player
{
    [SyncVar]
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
}

