using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BCIHTTPConnector : Singleton<BCIHTTPConnector>
{
	public Text StatusText;
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
		UnityWebRequest www = UnityWebRequest.Get("http://bci.ngrok.io");
		yield return www.SendWebRequest();
		if (www.isNetworkError || www.isHttpError)
		{
//			Debug.Log("It errored");
//			Debug.Log(www.error);
		}
		else
		{
//			Debug.Log("It succeeded:");
//			Debug.Log(www.responseCode);
//			Debug.Log("Resp text:");
			bool isTensed = www.downloadHandler.text == "true";
			StatusText.color = isTensed ? Color.green : Color.blue;
			if (isTensed)
			{
				_isActivated = true;
				// TODO: do we de-activate after a certain # of false checks?
			}
		}
	}
}