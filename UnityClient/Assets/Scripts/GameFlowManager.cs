using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager> {

    public GameObject rocketObject;
    public int rocketsToLaunch;
    private int rocketsLaunched;
    public GlobalGameState CurrentGameState;

    // Use this for initialization
    void Start () {
        rocketsLaunched = 0;
        CurrentGameState = GlobalGameState.NOTSTARTED;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncrementRocket()
    {
        if(CurrentGameState == GlobalGameState.LAUNCHROCKETS)
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
    NOTSTARTED, PLACEELEVATOR, SELECTEXERCISE, PLACESENSORS, LAUNCHROCKETS, NARRATIVE
}
