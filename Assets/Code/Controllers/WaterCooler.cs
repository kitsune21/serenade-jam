using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WaterCooler : MonoBehaviour {

	public Text NpcText;
	public Text PlayerText;
	public GameObject ResponsePanel;
	public Button ResponseButton;
	public Button CategoryButton;
	public Button BackButton;
	public PlayerKnownResponses player;
	private List<Button> PanelButtons = new List<Button>();
	private string prompt;
	private string playerResponse;

    // this comes from npcs?
    private Dictionary<string, List<string>> prompts = new Dictionary<string, List<string>>();

	private void CreatePrompts() {
		var categories = new List<string>();

		foreach(var c in SpeechPrompts.Categories.Keys) {
			categories.Add(c);
		}

		var cat = categories[Random.Range(0, categories.Count)];

		var NpcChoices = new List<string>();
		var chosenCat = SpeechPrompts.Categories[cat];

		foreach(var p in chosenCat.Keys){
			NpcChoices.Add(p);
		}

		int count = NpcChoices.Count;

		for (var i = 0; i < count / 2; i++) {
			var prompt = NpcChoices[Random.Range(0, NpcChoices.Count)];
			prompts.Add(prompt, chosenCat[prompt]);
			NpcChoices.Remove(prompt);
		}
	}

    private void Start() {
		CreatePrompts();


	    NpcPrompt();
    }

    private void NpcPrompt() {
	    List<string> npcPrompts = new List<string>();
	    if (prompts.Count == 0) {
			string exit = SpeechPrompts.Exits[Random.Range(0, SpeechPrompts.Exits.Count)];
			SetNpcText(exit);
			return;
	    }

	    foreach (var s in prompts.Keys) {
		    npcPrompts.Add(s);
	    }

	    prompt = npcPrompts[Random.Range(0, npcPrompts.Count)];

	    SetPlayerText("");
	    SetNpcText(prompt);

	    InstantiateButtons((GetResponseCategories()), CategoryButton);
	}

    private void SetNpcText(string text) {
	    NpcText.text = text;
		player.LearnPrompt(text);
    }

    private void SetPlayerText(string text) {
	    PlayerText.text = text;
    }

	private List<string> GetResponseCategories() {
	    var responseCategories = new List<string>();
	    foreach (var c in player.knownResponses.Keys) {
		    responseCategories.Add(c);
	    }

	    return responseCategories;
    }

    private void InstantiateButtons(List<string> responses, Button buttonType) {
	    foreach (var r in responses) {
		    Button response = Instantiate(buttonType, ResponsePanel.transform) as Button;
		    response.GetComponentInChildren<Text>().text = r;
		    response.GetComponent<ButtonScript>().ButtonText = r;
		    PanelButtons.Add(response);
	    }

	    if (buttonType == ResponseButton) {
		    Button back = Instantiate(BackButton, ResponsePanel.transform) as Button;
		    back.GetComponentInChildren<Text>().text = "Back";
		    PanelButtons.Add(back);
	    }
    }

    public void OpenCategory(GameObject category) {
	    string cat = category.GetComponent<ButtonScript>().ButtonText;

	    RemoveButtonsFromPanel();

	    InstantiateButtons(player.knownResponses[cat], ResponseButton);
    }

	public void HandleBack() {
		RemoveButtonsFromPanel();
		InstantiateButtons(GetResponseCategories(), CategoryButton);
    }

    public void MakeResponse(GameObject response) {
	    playerResponse = response.GetComponent<ButtonScript>().ButtonText;

		SetNpcText("");
		SetPlayerText(playerResponse);

		RemoveButtonsFromPanel();

		StartCoroutine(CheckResponse());
    }

	IEnumerator CheckResponse() {
		yield return new WaitForSeconds(1);

		if (prompts[prompt].Contains(playerResponse)) {
			SetNpcText("Good job!");
		} else {
			SetNpcText("Booo!");
		}

		prompts.Remove(prompt);

		StartCoroutine(NewPrompt());
	}

	IEnumerator NewPrompt() {
	    yield return new WaitForSeconds(2);
	    NpcPrompt();
    }

	
    private void RemoveButtonsFromPanel() {
	    foreach (Transform child in ResponsePanel.transform) {
		    GameObject.Destroy(child.gameObject);
	    }
    }

}
