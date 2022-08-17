using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Playables;

public class GameManager : Singleton<GameManager>
{
	public GameMode gameMode = GameMode.Gameplay;
	private PlayableDirector activeDirector;

	[SerializeField]
	private CTInputManager inputManager;

	private void Awake()
	{
		inputManager = CTInputManager.Instance;
	}

	private void OnEnable()
	{
		inputManager.OnTapPrimary += ResumeTimeline;
	}

	private void OnDisable()
	{
		inputManager.OnTapPrimary -= ResumeTimeline;
	}

	//Called by the TimeMachine Clip (of type Pause)
	public void PauseTimeline(PlayableDirector whichOne)
	{
		activeDirector = whichOne;
		activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
		gameMode = GameMode.DialogueMoment; //InputManager will be waiting for a spacebar to resume
		UIManager.Instance.TogglePressSpacebarMessage(true);
	}

	//Called by the InputManager
	public void ResumeTimeline(Vector2 position, float time)
	{
		UIManager.Instance.TogglePressSpacebarMessage(false);
		Debug.Log("In Resume TimeLine");
		GameObject.Destroy(UIManager.Instance.tempBubble);
		activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
		gameMode = GameMode.Gameplay;
	}

	public enum GameMode
	{
		Gameplay,
		//Cutscene,
		DialogueMoment, //waiting for input
	}
}
