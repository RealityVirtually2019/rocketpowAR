using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager>
{
	public GameObject rocketObject;
	public int rocketsToLaunch;
	private int rocketsLaunched;
	
	public GameObject ElevatorPrefab;
	
	private GlobalGameState _currentGameState;
	
	private GameObject _elevator;

	public GlobalGameState GetGameState()
	{
		return _currentGameState;
	}
	
	void Start()
	{
		rocketsLaunched = 0;
		GoToState(GlobalGameState.WAIT_TO_BEGIN);
	}

	void Update()
	{
		if (_currentGameState == GlobalGameState.WAIT_TO_BEGIN)
		{
			if (_elevator != null)
			{
				Destroy(_elevator);
				_elevator = null;
			}
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
				Transform controllerRaycast = InputManager.Instance.ControllerRaycast;
				_elevator = Instantiate(ElevatorPrefab, controllerRaycast.position, Quaternion.identity);
				_elevator.GetComponent<ElevatorDoors>().openElevatorDoors();
				// TODO: Make plane orient relative to player
				
				controllerRaycast.gameObject.SetActive(false);
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
				GoToState(GlobalGameState.WAIT_TO_BEGIN);
			}
		}
		else if (_currentGameState == GlobalGameState.PLACE_ELECTRODES)
		{
			
			UIManager.Instance.BigCenterText.text = "";
			UIManager.Instance.HintText.text = "PLACE ELECTRODES";
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
			AudioManager.Instance.PlayMusic(AudioLibrary.Instance.AmbientIntro);
		}
		if (stateBeingEntered == GlobalGameState.PLACE_ELEVATOR)
		{
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.NarrativePlaceElevator);
		}
		if (stateBeingEntered == GlobalGameState.SELECT_EXERCISE)
		{
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.NarrativeSelectExercise);
		}
		if (stateBeingEntered == GlobalGameState.PLACE_ELECTRODES)
		{
//			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.NarrativeIntro);
		}
		if (stateBeingEntered == GlobalGameState.TRANSITION_TO_COMMAND_CENTER)
		{
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.NarrativeIntro);
		}
	}

	public void IncrementRocket()
	{
		if (_currentGameState == GlobalGameState.LAUNCH_ROCKETS)
		{
			if (rocketsLaunched < rocketsToLaunch)
			{
				rocketsLaunched++;
			}
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
}