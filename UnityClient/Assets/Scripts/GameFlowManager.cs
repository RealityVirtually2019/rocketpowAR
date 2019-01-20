using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager>
{
	public Transform PlaySpaceRoot;
	public Transform Rocket;
	public Transform BodyFull;
	public Transform Torso;
	
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
				Rocket.gameObject.SetActive(false);
				BodyFull.transform.gameObject.SetActive(true);
				Torso.transform.gameObject.SetActive(false);
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
				// TODO: Add transition effect!
				BodyFull.transform.gameObject.SetActive(false);
				Torso.transform.gameObject.SetActive(true);
				GoToState(GlobalGameState.NICE);
			}
		}
		else if (_currentGameState == GlobalGameState.NICE)
		{
			UIManager.Instance.BigCenterText.text = "";
			UIManager.Instance.HintText.text = "";
			if (!AudioManager.Instance.IsNarrativeStillPlaying() || Input.GetKeyDown(KeyCode.F))
			{
				GoToState(GlobalGameState.PLACE_ELECTRODES);
			}
		}
		else if (_currentGameState == GlobalGameState.PLACE_ELECTRODES)
		{
			UIManager.Instance.BigCenterText.text = "";
			UIManager.Instance.HintText.text = "PLACE ELECTRODES";
			if ((!AudioManager.Instance.IsNarrativeStillPlaying() && InputManager.Instance.IsTriggerDown()) || Input.GetKeyDown(KeyCode.F))
			{
				GoToState(GlobalGameState.PLACE_ELECTRODES_2);
			}
		}
		else if (_currentGameState == GlobalGameState.PLACE_ELECTRODES_2)
		{
			UIManager.Instance.BigCenterText.text = "";
			UIManager.Instance.HintText.text = "PLACE ELECTRODES";
			if ((!AudioManager.Instance.IsNarrativeStillPlaying() && InputManager.Instance.IsTriggerDown()) || Input.GetKeyDown(KeyCode.F))
			{
				GoToState(GlobalGameState.ROCKET_TRANSITION);
			}
		}
		else if (_currentGameState == GlobalGameState.ROCKET_TRANSITION)
		{
			UIManager.Instance.BigCenterText.text = "";
			UIManager.Instance.HintText.text = "";
			if (!AudioManager.Instance.IsNarrativeStillPlaying() || Input.GetKeyDown(KeyCode.F))
			{
				GoToState(GlobalGameState.LAUNCH_ROCKETS);
			}
		}
		else if (_currentGameState == GlobalGameState.LAUNCH_ROCKETS)
		{
			UIManager.Instance.BigCenterText.text = "";
			UIManager.Instance.HintText.text = "";
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
			AudioManager.Instance.StopMusic();
			AudioManager.Instance.PlayAmbient(AudioLibrary.Instance.AmbientIntro);
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.NarrativePlaceElevator);
		}
		else if (stateBeingEntered == GlobalGameState.SELECT_EXERCISE)
		{
			AudioManager.Instance.PlaySoundEffect(AudioLibrary.Instance.ElevatorDoorOpen);
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.NarrativeWelcomeGreeting, 4.3f);
			StartCoroutine(delayedAnimation(ElevatorDoor, "ElevatorDoorCloseNew", 4.3f));

		}
		else if (stateBeingEntered == GlobalGameState.NICE)
		{
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.ChosenItem);
			AudioManager.Instance.PlaySoundEffect(AudioLibrary.Instance.TorsoTransition);
		}
		else if (stateBeingEntered == GlobalGameState.PLACE_ELECTRODES)
		{
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.SensorIntroduction1);
		}
		else if (stateBeingEntered == GlobalGameState.PLACE_ELECTRODES_2)
		{
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.SensorIntroduction2);
			StartCoroutine(delayedAnimation(ElevatorDoor, "ElevatorDoorOpenNew", 0f));
		}
		else if (stateBeingEntered == GlobalGameState.ROCKET_TRANSITION)
		{
			MenuScene.Play("HumanTubeLower");
			Rocket.gameObject.SetActive(true);
			Rocket.transform.localPosition = Vector3.zero;
			StartCoroutine(delayedAnimation(ElevatorDoor, "ElevatorDoorCloseNew", 1.5f));
			AudioManager.Instance.PlayNarrative(AudioLibrary.Instance.SensorSet);
			AudioManager.Instance.PlaySoundEffect(AudioLibrary.Instance.ElevatorDoorOpen);
		}
		else if (stateBeingEntered == GlobalGameState.LAUNCH_ROCKETS)
		{
			AudioManager.Instance.PlaySoundEffect(AudioLibrary.Instance.CrowdHospitalWaiting);
			AudioManager.Instance.PlayAmbient(AudioLibrary.Instance.IndustrialSpaces);
			RocketLaunchMiniGame.Instance.StartMiniGame();
		}
	}
	
	IEnumerator delayedAnimation (Animator animator, string animationName, float startDelay)
	{
		yield return new WaitForSeconds(startDelay);
		animator.Play(animationName);
	}
}
     

public enum GlobalGameState
{
	NOT_STARTED,
	WAIT_TO_BEGIN,
	PLACE_ELEVATOR,
	MENU_LOAD,
	SELECT_EXERCISE,
	NICE,
	PLACE_ELECTRODES,
	PLACE_ELECTRODES_2,
	ROCKET_TRANSITION,
	LAUNCH_ROCKETS,
	GAME_WON,
	NARRATIVE,
	WIN,
}