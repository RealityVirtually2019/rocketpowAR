using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
	public AudioSource MusicAudioSource;
	public AudioSource NarrativeAudioSource;

	public void PlayMusic(AudioClip audioToPlay)
	{
		MusicAudioSource.clip = audioToPlay;
		MusicAudioSource.Play();
	}
	
	public bool IsMusicStillPlaying()
	{
		return MusicAudioSource.isPlaying;
	}
	
	public void PlayNarrative(AudioClip audioToPlay)
	{
		NarrativeAudioSource.clip = audioToPlay;
		NarrativeAudioSource.Play();
	}
	
	public bool IsNarrativeStillPlaying()
	{
		return NarrativeAudioSource.isPlaying;
	}
}