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
	private List<Button> PanelButtons = new List<Button>();
	private string prompt;
	private string playerResponse;

    // this will change with more prompts
    private Dictionary<string, List<string>> prompts = SpeechPrompts.SportsPrompts;

    // this will come from the Player controller
    private static readonly List<string> knownSportsResponses = new List<string>() {
	    "Football",
	    "Ah yes, I sports talk",
		"Nay brother. I donteth.",
	    "It was ludicrous"
    };

    private static readonly List<string> knownGeneralResponses = new List<string>() {
	    "Cool story.",
	    "I could go for some pizza.",
	    "Neat."
    };

    private readonly Dictionary<string, List<string>> knownResponses = new Dictionary<string, List<string>>() {
	    {"Sports", knownSportsResponses},
	    {"General", knownGeneralResponses}
    };

    private void Start() {
	    NpcPrompt();
    }

    private void NpcPrompt() {
	    List<string> npcPrompts = new List<string>();
	    if (prompts.Count == 0) {
			SetNpcText("All Done!");
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
    }

    private void SetPlayerText(string text) {
	    PlayerText.text = text;
    }

	private List<string> GetResponseCategories() {
	    var responseCategories = new List<string>();
	    foreach (var c in knownResponses.Keys) {
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

	    InstantiateButtons(knownResponses[cat], ResponseButton);
    }

	public void HandleBack() {
		Debug.Log("Handle Back");
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

		if (SpeechPrompts.SportsPrompts[prompt].Contains(playerResponse)) {
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
