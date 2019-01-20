using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager>
{
	public Transform PlaySpaceRoot;
	
	public Animator ElevatorDoor;
	public Animator MenuScene;
	public Animator CommandRoomScene;

	private GlobalGameState _currentGameState;

	private GameObject _elevator;

	public GlobalGameState GetGameState()
	{
		return _currentGameState;
	}

	void Start()
	{
		GoToState(GlobalGameState.WAIT_TO_BEGIN);
	}

	void Update()
	{
		if (_currentGameState == GlobalGameState.WAIT_TO_BEGIN)
		{
			UIManager.Instance.BigCenterText.text = "PRESS TRIGGER TO BEGIN";
			UIManager.Instance.HintText.text = "";
			if (InputManager.Instance.IsTriggerDown())
			{
				GoToState(GlobalGameState.PLACE_ELEVATOR);
			}
		}
		else if (_currentGameState == GlobalGameState.PLACE_ELEVATOR)
		{
			UIManager.Instance.BigCenterText.text = "";
			UIManager.Instance.HintText.text = "PLACE ON THE FLOOR (PRESS TRIGGER)";
			if (InputManager.Instance.IsTriggerDown())
			{
				PlaySpaceRoot.gameObject.SetActive(true);
				Transform controllerRaycast = InputManager.Instance.ControllerRaycast;
				PlaySpaceRoot.transform.position = controllerRaycast.position;
				PlaySpaceRoot.transform.LookAt(Camera.main.transform);
				PlaySpaceRoot.transform.localEulerAngles = new Vector3(0f, PlaySpaceRoot.transform.localEulerAngles.y, 0f);
				ElevatorDoor.Play("ElevatorDoorOpenNew");
				controllerRaycast.gameObject.SetActive(false);
				GoToState(GlobalGameState.SELECT_EXERCISE);
			}
		}
		else if (_currentGameState == GlobalGameState.SELECT_EXERCISE)
		{
			UIManager.Instance.BigCenterText.text = "";
			UIManager.Instance.HintText.text = "SELECT YOUR EXERCISE";
			if (InputManager.Instance.IsTriggerDown())
			{
				GoToState(GlobalGameState.TRANSITION_TO_COMMAND_CENTER);
			}
		}
		else if (_currentGameState == GlobalGameState.TRANSITION_TO_COMMAND_CENTER)
		{
			UIManager.Instance.BigCenterText.text = "";
			UIManager.Instance.HintText.text = "[TRANSITION]";
			if (InputManager.Instance.IsTriggerDown()
				// And animation is finished
			)
			{
				GoToState(GlobalGameState.PLACE_ELECTRODES);
			}
		}
		else if (_currentGameState == GlobalGameState.PLACE_ELECTRODES)
		{
			UIManager.Instance.BigCenterText.text = "";
			UIManager.Instance.HintText.text = "PLACE ELECTRODES";
			if (InputManager.Instance.IsTriggerDown())
			{
				GoToState(GlobalGameState.LAUNCH_ROCKETS);
			}
		}
		else if (_currentGameState == GlobalGameState.LAUNCH_ROCKETS)
		{
			UIManager.Instance.BigCenterText.text = "";
			UIManager.Instance.HintText.text = "LAUNCH ROCKETS";
			if (RocketLaunchMiniGame.Instance.RocketLaunchGameState == MiniGameState.COMPLETED)
			{
				// WIN!
				RocketLaunchMiniGame.Instance.ResetState();
				GoToState(GlobalGameState.WIN);
			}
		}
		else if (_currentGameState == GlobalGameState.WIN)
		{
			UIManager.Instance.BigCenterText.text = "";
			UIManager.Instance.HintText.text = "CONGRATS!";
			if (InputManager.Instance.IsTriggerDown())
			{
				GoToState(GlobalGameState.WAIT_TO_BEGIN);
			}
		}
	}

	private void GoToState(GlobalGameState toState)
	{
		if (_currentGameState != toState)
		{
			print("Going to state " + toState.ToString());
			onStateEnter(toState);

			_currentGameState = toState;
		}
	}

	private void onStateEnter(GlobalGameState stateBeingEntered)
	{
		if (stateBeingEntered == GlobalGameState.WAIT_TO_BEGIN)
		{
			PlaySpaceRoot.gameObject.SetActive(false);
			AudioManager.Instance.PlayMusic(AudioLibrary.Instance.SplashScreenMusic);
		}
		else if (stateBeingEntered == GlobalGameState.PLACE_ELEVATOR)
		{
			AudioManager.Instance.PlayMusic(AudioLibrary.Instance.AmbientIntro);
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.NarrativePlaceElevator);
		}
		else if (stateBeingEntered == GlobalGameState.SELECT_EXERCISE)
		{
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.NarrativeSelectExercise);
		}
		else if (stateBeingEntered == GlobalGameState.PLACE_ELECTRODES)
		{
//			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.NarrativeIntro);
		}
		else if (stateBeingEntered == GlobalGameState.TRANSITION_TO_COMMAND_CENTER)
		{
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.NarrativeIntro);
		}
		else if (stateBeingEntered == GlobalGameState.LAUNCH_ROCKETS)
		{
			RocketLaunchMiniGame.Instance.StartMiniGame();
		}
	}
}

public enum GlobalGameState
{
	NOT_STARTED,
	WAIT_TO_BEGIN,
	PLACE_ELEVATOR,
	SELECT_EXERCISE,
	TRANSITION_TO_COMMAND_CENTER,
	PLACE_ELECTRODES,
	LAUNCH_ROCKETS,
	GAME_WON,
	NARRATIVE,
	WIN
}