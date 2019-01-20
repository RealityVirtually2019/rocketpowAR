using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BCIHTTPConnector : MonoBehaviour {

	void Start()
	{
		StartCoroutine(MakeDataRequest());
	}

	IEnumerator MakeDataRequest()
	{
		UnityWebRequest www = UnityWebRequest.Get("http://93b9d778.ngrok.io/");
		Debug.Log("Web Request Started");
		yield return www.SendWebRequest();
		Debug.Log("Web Request Ended");
		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log("It errored");
			Debug.Log(www.error);
		}
		else
		{
			Debug.Log("It succeeded:");
			Debug.Log(www.responseCode);
			Debug.Log("Resp text:");
			Debug.Log(www.downloadHandler.text);
			// Or retrieve results as binary data
//			byte[] results = www.downloadHandler.data;
		}
	}
}
