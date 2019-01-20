using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : Singleton<AudioLibrary>
{
	[Header("Narrative Clips")]
	public AudioClip NarrativePlaceElevator;
	public AudioClip NarrativeSelectExercise;
	public AudioClip NarrativeIntro;
	
	[Header("Music Clips")]
	public AudioClip AmbientIntro;
}