using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dpoch.SocketIO;

public class BCISocketConnector : MonoBehaviour
{
    void Start()
    {
        SocketIO socket = new SocketIO("ws://127.0.0.1:/socket.io/?EIO=4&transport=websocket");
//        SocketIO socket = new SocketIO("ws://10.189.1.40:3000/socket.io/?EIO=4&transport=websocket");

        float startTime = Time.realtimeSinceStartup;

        socket.On("channel-data", (ev) =>
        {
            int channelID = ev.Data[0].ToObject<int>();
            float value = ev.Data[1].ToObject<float>();
            Debug.Log(channelID);
            Debug.Log(value);
        });

        socket.OnOpen += () => { Debug.Log("Opened"); };
        socket.OnConnectFailed += () => Debug.Log("Socket failed to connect!");
        socket.OnClose += () =>
        {
            float elapsed = Time.realtimeSinceStartup - startTime;
            Debug.Log("Time left: " + elapsed);
            Debug.Log("Socket closed!");
        };
        socket.OnError += (err) =>
        {
            float elapsed = Time.realtimeSinceStartup - startTime;
            Debug.Log("Time left: " + elapsed);
            Debug.Log("Socket Error: " + err);
        };

        socket.Connect();
    }
}