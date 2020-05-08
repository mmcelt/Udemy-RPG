using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	#region Fields
	public static AudioManager Instance;

	public AudioSource[] _music;
	public AudioSource[] _sfx;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		
	}
	
	void Update() 
	{
	}
	#endregion

	#region Public Methods

	public void PlaySFX(int soundToPlay)
	{
		if (soundToPlay < _sfx.Length)
			_sfx[soundToPlay].Play();
	}

	public void PlayMusic(int musicToPlay)
	{
		if (_music[musicToPlay].isPlaying) return;

		Stopmusic();

		if (musicToPlay < _music.Length)
			_music[musicToPlay].Play();
	}

	public void Stopmusic()
	{
		foreach (AudioSource music in _music)
			music.Stop();
	}
	#endregion

	#region Private Methods


	#endregion
}
