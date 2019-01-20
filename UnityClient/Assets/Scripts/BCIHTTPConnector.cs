using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BCIHTTPConnector : Singleton<BCIHTTPConnector>
{
	private bool _isActivated;

	public void ResetIsActivated()
	{
		_isActivated = false;
	}
	
	public bool CheckIsActivated()
	{
		if (_isActivated)
		{
			_isActivated = false;
			return true;
		}
		return false;
	}
	
	void Start()
	{
		InvokeRepeating("SingleDataRequest", .4f, .4f);
	}

	void SingleDataRequest()
	{
		StartCoroutine(MakeDataRequest());
	}

	IEnumerator MakeDataRequest()
	{
		UnityWebRequest www = UnityWebRequest.Get("http://b3a6a049.ngrok.io/");
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
			if (www.downloadHandler.text == "true")
			{
				_isActivated = true;
				// TODO: do we de-activate after a certain # of false checks?
			}
		}
	}
}