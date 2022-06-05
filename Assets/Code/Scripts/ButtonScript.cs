using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
	[HideInInspector] public string ButtonText;
	[HideInInspector] public string SpeechType;

	public void Select() {
		FindObjectOfType<WaterCooler>().MakeResponse(gameObject);
	}

	public void OpenCategory() {
		if (SpeechType == "response") {
			FindObjectOfType<WaterCooler>().OpenCategory(gameObject);
		} else {
			FindObjectOfType<NpcDialogue>().OpenCategory(gameObject);
		}
		
	}

	public void HandleBack() {
		if (SpeechType == "response") {
			FindObjectOfType<WaterCooler>().HandleBack();
		} else {
			FindObjectOfType<NpcDialogue>().HandleBack();
		}
		
	}

	public void MakePrompt() {
		FindObjectOfType<NpcDialogue>().MakePrompt(gameObject);
	}
}
