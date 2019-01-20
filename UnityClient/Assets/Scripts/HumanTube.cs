using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanTube : MonoBehaviour {

    public GameObject humanTube;
    public Vector3 humanTubeBelowElevator;
    public Vector3 humanTubeRaised;

    public AudioSource raiseHumanTubeSFX;
    public AudioSource lowerHumanTubeSFX;

	// Use this for initialization
	void Start () {
        humanTube.transform.position = humanTubeBelowElevator;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaiseHumanTube();
        }
    }

    public void RaiseHumanTube()
    {
        if (GameFlowManager.Instance.GetGameState() == GlobalGameState.SELECT_EXERCISE)
        {
            raiseHumanTubeSFX.Play();
            humanTube.transform.position = Vector3.Lerp(humanTubeBelowElevator, humanTubeRaised, 2000);
        }
        /*else
        {
            Debug.Log("Cannot raise humanTube from elevator; game not in SELECTEXERCISE state.");
        }*/
    }

    public void LowerHumanTube()
    {
        if (GameFlowManager.Instance.GetGameState() == GlobalGameState.SELECT_EXERCISE)
        {
            lowerHumanTubeSFX.Play();
            humanTube.transform.position = Vector3.Lerp(humanTubeRaised, humanTubeBelowElevator, 2000);
        }
    }
}
