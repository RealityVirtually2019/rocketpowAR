using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLaunchMiniGame : Singleton<RocketLaunchMiniGame>
{
	public MiniGameState RocketLaunchGameState;
	private int _activityALaunches = 0;
	private int _activityBLaunches = 0;
	
	private ActivityAState _activityAState;
	private ActivityBState _activityBState;

	private const float ActivityADuration = 8f;
	private float _activityAStartTime;
	
	public void StartMiniGame()
	{
		GoToState(MiniGameState.INTRODUCTION);
	}

	void Update()
	{
		if (RocketLaunchGameState == MiniGameState.INTRODUCTION)
		{
			if (!AudioManager.Instance.IsNarrativeStillPlaying() && InputManager.Instance.IsTriggerDownThisFrame() || Input.GetKeyDown(KeyCode.F))
			{
				BCIHTTPConnector.Instance.ResetIsActivated();
				GoToState(MiniGameState.ACTIVITY_A);
			}
		}
		else if (RocketLaunchGameState == MiniGameState.ACTIVITY_A)
		{
			if (_activityAState == ActivityAState.NOT_STARTED)
			{
				AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.ActivityInstructionA);
				GoToActivityAState(ActivityAState.INSTRUCTIONS);
			}
			else if (_activityAState == ActivityAState.INSTRUCTIONS)
			{
				if (!AudioManager.Instance.IsNarrativeStillPlaying() || Input.GetKeyDown(KeyCode.F))
				{
					print("Narrative not playing");
					_activityAStartTime = Time.realtimeSinceStartup;
					GoToActivityAState(ActivityAState.FLEXING);
				}
			}
			else if (_activityAState == ActivityAState.FLEXING)
			{
				print("In flexing");
				float timeSoFar = Time.realtimeSinceStartup - _activityAStartTime;
				print(timeSoFar);
				print(ActivityADuration);
				if (timeSoFar > ActivityADuration)
				{
					GoToState(MiniGameState.ACTIVITY_B);					
				}
			}
		}
		else if (RocketLaunchGameState == MiniGameState.ACTIVITY_B)
		{
			if (_activityBState == ActivityBState.NOT_STARTED)
			{
				AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.ActivityInstructionB);
				GoToActivityBState(ActivityBState.INSTRUCTIONS);
			}
			else if (_activityBState == ActivityBState.INSTRUCTIONS)
			{
				if (!AudioManager.Instance.IsNarrativeStillPlaying() || Input.GetKeyDown(KeyCode.F))
				{
					BCIHTTPConnector.Instance.ResetIsActivated();
					GoToActivityBState(ActivityBState.EXERCISING);
				}
			}
			else if (_activityBState == ActivityBState.EXERCISING)
			{
				if (BCIHTTPConnector.Instance.CheckIsActivated())
				{
					GameFlowManager.Instance.SmokeSystem.gameObject.SetActive(true);
				}
					
				if (InputManager.Instance.IsTriggerDownThisFrame())
				{
					GoToActivityBState(ActivityBState.LAUNCHING);
					GameFlowManager.Instance.CommandRoomScene.Play("LaunchRocket");
				}
			}
			else if (_activityBState == ActivityBState.LAUNCHING)
			{
				GameFlowManager.Instance.FireSystem.gameObject.SetActive(true);
				GameFlowManager.Instance.SmokeSystem.gameObject.SetActive(true);
				GameFlowManager.Instance.Rocket.transform.Translate(new Vector3(0, .1f, 0));
				// Trigger Rogue's animation here!
				if (InputManager.Instance.IsTriggerDownThisFrame()) {
					GoToState(MiniGameState.COMPLETED);
				}
			}
		}
	}
	
	private void GoToState(MiniGameState toState)
	{
		if (RocketLaunchGameState != toState)
		{
			print("Going to state " + toState.ToString());
			onStateEnter(toState);
			
			RocketLaunchGameState = toState;
		}
	}

	private void GoToActivityAState(ActivityAState toState)
	{
		if (_activityAState != toState)
		{
			print("Going to Activity A state " + toState.ToString());
			_activityAState = toState;
		}
	}

	private void GoToActivityBState(ActivityBState toState)
	{
		if (_activityBState != toState)
		{
			print("Going to Activity B state " + toState.ToString());
			_activityBState = toState;
		}
	}
	
	private void onStateEnter(MiniGameState stateBeingEntered)
	{
		if (stateBeingEntered == MiniGameState.INTRODUCTION)
		{
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.NarrativeIntro);	
		}
		else if (stateBeingEntered == MiniGameState.ACTIVITY_A)
		{
			GoToActivityAState(ActivityAState.NOT_STARTED);	
		}
		else if (stateBeingEntered == MiniGameState.ACTIVITY_B)
		{
			GoToActivityBState(ActivityBState.NOT_STARTED);	
		}
	}

	public void ResetState()
	{
		RocketLaunchGameState = MiniGameState.NOT_STARTED;
	}
}

public enum MiniGameState
{
	NOT_STARTED,
	INTRODUCTION,
	ACTIVITY_A,
	ACTIVITY_B, // 10 flex and raise
	COMPLETED
}

public enum ActivityAState
{
	NOT_STARTED,
	INSTRUCTIONS,
	FLEXING
}

public enum ActivityBState
{
	NOT_STARTED,
	INSTRUCTIONS,
	EXERCISING,
	RAISING,
	LAUNCHING
}
