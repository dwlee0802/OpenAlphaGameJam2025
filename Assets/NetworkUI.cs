using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using TMPro;
using System;

public class NetworkUI : MonoBehaviour
{
    public bool networkEstablished = false;

    [SerializeField]
    Button hostButton;
    [SerializeField]
    Button joinButton;
    [SerializeField]
    TMP_Text joinCodeLabel;
    [SerializeField]
    TMP_InputField field;

    bool flag = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        hostButton.onClick.AddListener(HostButtonOnClick);
        joinButton.onClick.AddListener(JoinButtonOnClick);

        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    async void HostButtonOnClick()
    {
        if (flag)
        {
            return;
        }
        flag = true;
        string code = await StartHostWithRelay();
        GameManager.instance.preGameUI.codeLabel.text = "Join Code: " + code;
        gameObject.SetActive(false);
        GameManager.instance.InGameUI.gameObject.SetActive(true);
        networkEstablished = true;
    }

    async void JoinButtonOnClick()
    {
        if (flag)
        {
            return;
        }
        flag = true;
        string code = field.text;

        try
        {
            await StartClientWithRelay(code);
        }
        catch (Exception e)
        {

        }

        gameObject.SetActive(false);
        GameManager.instance.InGameUI.gameObject.SetActive(true);
        networkEstablished = true;
    }

    public async Task<string> StartHostWithRelay(int maxConnections = 5)
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));

        var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        print("Code: " + joinCode);

        return NetworkManager.Singleton.StartHost() ? joinCode : null;
    }

    public async Task<bool> StartClientWithRelay(string joinCode)
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
        return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient();
    }
}
