using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] GameObject onlinePage;
    [SerializeField] InputField addressInput;

    public void Join()
    {
        NetworkManager.singleton.networkAddress = addressInput.text;
        NetworkManager.singleton.StartClient();
    }
    private void Start()
    {
        CheckersNetworkManager.ClientOnConnected += HindleClientConnected;
    }
    private void OnDestroy()
    {
        CheckersNetworkManager.ClientOnConnected -= HindleClientConnected;

    }
    void HindleClientConnected()
    {
        onlinePage.SetActive(false);
        gameObject.SetActive(false);
    }

}
