// Date   : 10.12.2016 14:24
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using System.Collections.Generic;

public class DisplayMessageManager : MonoBehaviour
{

    [SerializeField]
    [Range(0.2f, 5f)]
    private float messageFadeTime = 1f;

    private Queue<string> messageQueue = new Queue<string>();

    [SerializeField]
    [Range(0f, 2f)]
    private float messageMinInterval = 0.6f;
    private float messageTimer = 0f;

    bool allowNewMessage = true;

    [SerializeField]
    private DisplayMessage displayMessagePrefab;

    [SerializeField]
    private Transform displayMessageContainer;

    private KeyCode keyConfirm = KeyCode.None;

    private List<DisplayMessage> messageList = new List<DisplayMessage>();

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyUp(keyConfirm)) {
            allowNewMessage = true;
        }
        if (!allowNewMessage)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0.01f)
            {
                allowNewMessage = true;
            }
        }
        else
        {
            if(messageQueue.Count > 0)
            {
                DisplayMessage(messageQueue.Dequeue());
                allowNewMessage = false;
                messageTimer = messageMinInterval;
            }
        }
        
    }

    void DisplayMessage(string message)
    {
        DisplayMessage displayMessage = Instantiate(displayMessagePrefab);
        displayMessage.Init(message, displayMessageContainer, messageFadeTime);
        messageList.Add(displayMessage);
        for (int i = messageList.Count - 1; i >= 0; i--)
        {
            messageList[i].MoveUp();
        }
        displayMessage.Show();
    }

    public void ShowMessage(string message)
    {
        if(keyConfirm == KeyCode.None)
        {
            keyConfirm = KeyManager.main.GetKey(Action.Confirm);
        }
        messageQueue.Enqueue(message);
    }

}
