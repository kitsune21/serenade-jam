using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
	[HideInInspector] public string ButtonText;

	public void Select() {
		FindObjectOfType<WaterCooler>().MakeResponse(gameObject);
	}

	public void OpenCategory() {
		FindObjectOfType<WaterCooler>().OpenCategory(gameObject);
	}

	public void HandleBack() {
		FindObjectOfType<WaterCooler>().HandleBack();
	}
}
