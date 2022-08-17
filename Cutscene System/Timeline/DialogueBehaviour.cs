using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
    public string characterName;
    public string dialogueLine;
    public int dialogueSize;
	public Vector2 bubblePosition;
	public Vector2 bubbleSize;

	public bool hasToPause = false;

	private bool clipPlayed = false;
	private bool pauseScheduled = false;
	private PlayableDirector director;

	public override void OnPlayableCreate(Playable playable)
	{
		director = (playable.GetGraph().GetResolver() as PlayableDirector);
	}

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		if(!clipPlayed
			&& info.weight > 0f)
		{
			UIManager.Instance.SetDialogue(characterName, dialogueLine, dialogueSize, bubblePosition, bubbleSize);

			if(Application.isPlaying)
			{
				if(hasToPause)
				{
					pauseScheduled = true;
				}
			}

			clipPlayed = true;
		}
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		if(pauseScheduled)
		{
			pauseScheduled = false;
			GameManager.Instance.PauseTimeline(director);
		}
		else
		{
			GameObject.Destroy(UIManager.Instance.tempBubble);
		}

		clipPlayed = false;
	}
}
