﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class InputManager : Singleton<InputManager>
{
	public Transform ControllerRaycast;
	public ControllerConnectionHandler ControllerConnectionHandler;

	private const float TriggerThrottleTime = 3f;

	private float _lastTriggerTime = 0f;

	public bool IsTriggerDown()
	{
		MLInputController mlInputController = ControllerConnectionHandler.ConnectedController;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			_lastTriggerTime = Time.realtimeSinceStartup;
			return true;
		}
		
		if (mlInputController == null)
		{
			return false;
		}
		
		bool isTriggerDown = mlInputController.TriggerValue > 0.2f;
		
		float timeSinceLastOne = Time.realtimeSinceStartup - _lastTriggerTime;
		bool shouldTrigger = isTriggerDown && timeSinceLastOne > TriggerThrottleTime;
		if (shouldTrigger) {
		    _lastTriggerTime = Time.realtimeSinceStartup;
		}
		return shouldTrigger;
	}
}