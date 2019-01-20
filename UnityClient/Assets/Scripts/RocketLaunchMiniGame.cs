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
	
	public void StartMiniGame()
	{
		GoToState(MiniGameState.ACTIVITY_A);
	}

	void Update()
	{
		if (RocketLaunchGameState == MiniGameState.ACTIVITY_A)
		{
			if (_activityAState == ActivityAState.NOT_STARTED)
			{
				AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.ActivityInstructionA);
				GoToActivityAState(ActivityAState.INSTRUCTIONS);
			}
			else if (_activityAState == ActivityAState.INSTRUCTIONS)
			{
				if (!AudioManager.Instance.IsNarrativeStillPlaying())
				{
					BCIHTTPConnector.Instance.ResetIsActivated();
					GoToActivityAState(ActivityAState.FLEXING);
				}
			}
			else if (_activityAState == ActivityAState.FLEXING)
			{
				// WHen we enter the flexing state, we raise the rocket
				// If they're activated over the network!
				if (BCIHTTPConnector.Instance.CheckIsActivated())
				{
					_activityALaunches++;
					if (_activityALaunches == 1)
					{
						AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.ActivityInstructionB);	
					}
					if (_activityALaunches == 2)
					{
						GoToState(MiniGameState.ACTIVITY_B);
					}
					else
					{
						GoToActivityAState(ActivityAState.INSTRUCTIONS);
					}
				}
			}
		}
		else if (RocketLaunchGameState == MiniGameState.ACTIVITY_B)
		{
			if (_activityBState == ActivityBState.NOT_STARTED)
			{
				// TODO: this might be wrong
				AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.ActivityInstructionC);
				GoToActivityBState(ActivityBState.INSTRUCTIONS);
			}
			else if (_activityBState == ActivityBState.INSTRUCTIONS)
			{
				if (!AudioManager.Instance.IsNarrativeStillPlaying())
				{
					BCIHTTPConnector.Instance.ResetIsActivated();
					GoToActivityBState(ActivityBState.FLEXING);
				}
			}
			else if (_activityBState == ActivityBState.FLEXING)
			{
				if (BCIHTTPConnector.Instance.CheckIsActivated())
				{
					GoToActivityBState(ActivityBState.RAISING);
				}
			}
			else if (_activityBState == ActivityBState.RAISING)
			{
				if (InputManager.Instance.IsRaisedToLaunchHeight())
				{
					// Update launch height based on distance btwn head 
					_activityBLaunches++;
					if (_activityBLaunches == 1)
					{
						AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.ActivityInstructionB);	
					}
					if (_activityBLaunches == 2)
					{
						GoToState(MiniGameState.COMPLETED);
					}
					else
					{
						GoToActivityBState(ActivityBState.INSTRUCTIONS);
					}
				}
			}
			else if (_activityBState == ActivityBState.LAUNCHING)
			{
				if (_activityBLaunches == 2)
				{
					GoToState(MiniGameState.COMPLETED);
				}
				else
				{
					GoToActivityBState(ActivityBState.INSTRUCTIONS);
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
		if (stateBeingEntered == MiniGameState.ACTIVITY_A)
		{
			GoToActivityAState(ActivityAState.FLEXING);	
		}
		else if (stateBeingEntered == MiniGameState.ACTIVITY_B)
		{
			GoToActivityBState(ActivityBState.INSTRUCTIONS);	
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
	ACTIVITY_A, // 3 flexes, first 2 with special instructions
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
	FLEXING,
	RAISING,
	LAUNCHING
}
