using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class CanvasManager : MonoBehaviourPunCallbacks
{
    public Button createButton;
    public Button joinRoomButton;
    public Button joinRandomRoomButton;
    public InputField roomNameField;
    public Text stateText;

    IEnumerator Start()
    {
        PhotonManager.Instance.ConnetToMaster();
        yield return new WaitUntil(() => { return PhotonManager.Instance.init; });
        ResetButton(true);
        createButton.onClick.AddListener(() =>
        {
            PhotonManager.Instance.CreateRoom(roomNameField.text);
            ResetButton(false);
            stateText.text = "Creating...";
            stateText.color = Color.gray;
        });

        joinRoomButton.onClick.AddListener(() => 
        {
            PhotonManager.Instance.JoinRoom(roomNameField.text);
            ResetButton(false);
            stateText.text = "joing...";
            stateText.color = Color.gray;
        });

        joinRandomRoomButton.onClick.AddListener(() =>
        {
            PhotonManager.Instance.JoinRandomRoom();
            ResetButton(false);
            stateText.text = "joing...";
            stateText.color = Color.gray;
        });
    }

    void ResetButton(bool value)
    {
        createButton.interactable = value;
        joinRandomRoomButton.interactable = value;
        joinRoomButton.interactable = value;
        roomNameField.interactable = value;
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        stateText.text = "Wait for Create or Join";
        stateText.color = Color.green;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        ResetButton(true);
        stateText.text = "There is no room can join!!";
        stateText.color = Color.red;
        roomNameField.text = "";
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        ResetButton(true);
        stateText.text = "The room do not exist!!";
        stateText.color = Color.red;
        roomNameField.text = "";
    }

}