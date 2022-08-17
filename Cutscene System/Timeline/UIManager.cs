using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
	public GameObject toggleSpacebarMessage;

	public GameObject bubblePrefab;

	public GameObject tempBubble;

	public Canvas mainCanvas;

	public GameObject SetDialogue(string charName, string lineOfDialogue, int sizeOfDialogue, Vector2 position, Vector2 size)
	{
		tempBubble = Instantiate(bubblePrefab, new Vector3(position.x, position.y, 0f), Quaternion.Euler(0f, 0f, 0f));
		tempBubble.transform.SetParent(mainCanvas.transform);
		tempBubble.transform.localPosition = new Vector3(position.x, position.y, 0f);
		tempBubble.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, size.y);
		tempBubble.transform.Find("BubbleText").GetComponent<RectTransform>().sizeDelta = new Vector2(tempBubble.GetComponent<RectTransform>().sizeDelta.x - 20f, tempBubble.GetComponent<RectTransform>().sizeDelta.y - 20f);


		tempBubble.transform.Find("DemoTextBubblePointer").GetComponent<RectTransform>().localPosition = new Vector3(tempBubble.transform.Find("DemoTextBubblePointer").GetComponent<RectTransform>().localPosition.x, tempBubble.transform.Find("DemoTextBubblePointer").GetComponent<RectTransform>().localPosition.y - ((tempBubble.GetComponent<RectTransform>().sizeDelta.y / 2) - 50f), 0f);


		tempBubble.GetComponentInChildren<TextMeshProUGUI>().SetText(lineOfDialogue);
		tempBubble.GetComponentInChildren<TextMeshProUGUI>().fontSize = sizeOfDialogue;

		return tempBubble;
	}

	public void TogglePressSpacebarMessage(bool active)
	{
		toggleSpacebarMessage.SetActive(active);
	}
}
