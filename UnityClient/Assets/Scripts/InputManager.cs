using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class InputManager : Singleton<InputManager>
{
	public Transform ControllerRaycast;
	public ControllerConnectionHandler ControllerConnectionHandler;

	private const float TriggerThrottleTime = .6f;

	private float _lastTriggerTime = 0f;
	private int _lastCheckedFrame;
	private bool _isTriggerDownThisFrame;

	public bool IsTriggerDownThisFrame()
	{
		if (_lastCheckedFrame == Time.frameCount)
		{
			return _isTriggerDownThisFrame;
		}
		_lastCheckedFrame = Time.frameCount;
		MLInputController mlInputController = ControllerConnectionHandler.ConnectedController;

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F))
		{
			_lastTriggerTime = Time.realtimeSinceStartup;
			_isTriggerDownThisFrame = true;
			return true;
		}
		
		if (mlInputController == null)
		{
			_isTriggerDownThisFrame = false;
			return false;
		}
		
		bool isTriggerDown = mlInputController.TriggerValue > 0.2f;
		
		float timeSinceLastOne = Time.realtimeSinceStartup - _lastTriggerTime;
		_isTriggerDownThisFrame = isTriggerDown && timeSinceLastOne > TriggerThrottleTime;
		if (_isTriggerDownThisFrame) {
		    _lastTriggerTime = Time.realtimeSinceStartup;
		}
		return _isTriggerDownThisFrame;
	}

	public bool IsRaisedToLaunchHeight()
	{
		float controllerPosition = ControllerConnectionHandler.transform.position.y;
		float headPosition = Camera.main.transform.position.y;
		bool isWithinThreshold = headPosition - controllerPosition < 1.0f;
		return isWithinThreshold;
	}
	
	public float GetRaiseAmount()
	{
		return .5f;
	}
}