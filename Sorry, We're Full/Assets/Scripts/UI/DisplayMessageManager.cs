// Date   : 10.12.2016 14:24
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using System.Collections.Generic;

public class DisplayMessageManager : MonoBehaviour {

    [SerializeField]
    [Range(0.2f, 5f)]
    private float messageFadeTime = 1f;

    [SerializeField]
    private DisplayMessage displayMessagePrefab;

    [SerializeField]
    private Transform displayMessageContainer;

    private List<DisplayMessage> messageList = new List<DisplayMessage>();

    void Start () {
    
    }

    public void ShowMessage(string message)
    {
        DisplayMessage displayMessage = Instantiate(displayMessagePrefab);
        displayMessage.Init(message, displayMessageContainer, messageFadeTime);
        messageList.Add(displayMessage);
        for (int i = messageList.Count - 1; i >= 0 ; i--)
        {
            messageList[i].MoveUp();
        }
        displayMessage.Show();
    }

}
