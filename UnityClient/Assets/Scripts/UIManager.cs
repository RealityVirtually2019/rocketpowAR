using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
	public Text BigCenterText;
	public Text HintText;
	public GameObject[] DebugUI;

	public void Start()
	{
		HideDebugUI();
	}
	
	public void HideDebugUI()
	{
		foreach (GameObject o in DebugUI)
		{
			o.SetActive(false);
		}
	}
}