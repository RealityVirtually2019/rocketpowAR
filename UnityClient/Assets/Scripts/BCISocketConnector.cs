using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;

public class BCISocketConnector : MonoBehaviour
{
//	public string SocketIOHost = "ws://10.189.1.40:3000";
	public string SocketIOHost = "ws://10.189.12.183:3000"; // 18.30.24.217
	public Transform[] TargetCubes;

	protected List<ChannelData> _dataQueue = new List<ChannelData>();
	private Socket _socket;

	void Destroy()
	{
		if (_socket != null)
		{
			Debug.Log("Disconnecting");
			_socket.Disconnect();
			_socket = null;
		}
	}

	void Start()
	{
		if (TargetCubes.Length < 4)
		{
			Debug.LogError("Must have at least 4 target cubes set.");
			return;
		}

		Debug.Log("Attempting to connect to socket.io");
		// Useful example of how to send data cross-thread.
		// https://github.com/floatinghotpot/socket.io-unity/blob/master/Demo/SocketIOScript.cs
		_socket = IO.Socket(SocketIOHost);
		_socket.On(Socket.EVENT_CONNECT, () => { Debug.Log("Connected to OpenBCI Connector."); });

		_socket.On("channel-data", data =>
		{
			lock (_dataQueue)
			{
				ChannelData channelData = JsonConvert.DeserializeObject<ChannelData>(data.ToString());
				_dataQueue.Add(channelData);
			}
		});
	}

	void Update()
	{
		lock (_dataQueue)
		{
			if (_dataQueue.Count > 0)
			{
				foreach (ChannelData data in _dataQueue)
				{
					if (data.value > 0)
					{
						Debug.Log(data.channelID);
						TargetCubes[data.channelID].localScale = Vector3.one * data.value;
					}
				}
				_dataQueue.Clear();
			}
		}
	}
}

public class ChannelData
{
	// Used for deserialization, ensure this structure matches that in Server/stream.js `io.emit` calls 
	public int channelID;

	public float value;
}