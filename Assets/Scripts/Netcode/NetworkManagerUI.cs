using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private TMP_InputField connectionCodeInput;
    [SerializeField] private Transform relay;

    // Update is called once per frame
    void Awake()
    {
        RelayConnection relayConnection = relay.GetComponent<RelayConnection>();
        hostBtn.onClick.AddListener(() => {
            relayConnection.CreateRelay();
        });

        clientBtn.onClick.AddListener(() => {
            relayConnection.JoinRelay(connectionCodeInput.text);
            Debug.Log(connectionCodeInput.text);
        });
    }
}
