using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WaterCooler : MonoBehaviour {

	public Text NpcText;
	public Text PlayerText;
	public GameObject TextPanel;
	public GameObject ResponsePanel;
	public GameObject TestingPanel;
	public Button ResponseButton;
	public Button CategoryButton;
	public Button BackButton;
	public PlayerKnownDialogue player;
	public NpcDialogue Npc;
	private List<Button> PanelButtons = new List<Button>();
	private string prompt;
	private string playerResponse;
	private string playerCategory;
	private string npcCategory;
	private Dictionary<string, List<string>> prompts;

	public void OpenWaterCooler() {
		TextPanel.SetActive(true);
		ResponsePanel.SetActive(true);
		TestingPanel.SetActive(false);
		prompts = new Dictionary<string, List<string>>(Npc.prompts);
		NpcPrompt();
	}

	private void Close() {
		RemoveButtonsFromPanel();
        TextPanel.SetActive(false);
        ResponsePanel.SetActive(false);
		TestingPanel.SetActive(true);
    }

    private void Start() {
	    TextPanel.SetActive(false);
		ResponsePanel.SetActive(false);
		player.LearnPrompt("Did you see that ludicrous display last night?");
        player.LearnPrompt("Do you know if Brian is dating anyone?");
        player.LearnPrompt("Someone walks into a bar.");
    }

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)){
			Close();
		}
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
			response.GetComponent<ButtonScript>().SpeechType = "response";
		    PanelButtons.Add(response);
	    }

	    if (buttonType == ResponseButton) {
		    Button back = Instantiate(BackButton, ResponsePanel.transform) as Button;
		    back.GetComponentInChildren<Text>().text = "Back";
			back.GetComponent<ButtonScript>().SpeechType = "response";
		    PanelButtons.Add(back);
	    }
    }

    public void OpenCategory(GameObject category) {
	    string cat = category.GetComponent<ButtonScript>().ButtonText;
		playerCategory = cat;

	    RemoveButtonsFromPanel();

	    InstantiateButtons(player.knownResponses[cat], ResponseButton);
    }

	public void HandleBack() {
		RemoveButtonsFromPanel();
		playerCategory = null;
		InstantiateButtons(GetResponseCategories(), CategoryButton);
    }

    public void MakeResponse(GameObject response) {
	    playerResponse = response.GetComponent<ButtonScript>().ButtonText;

		SetNpcText("");
		SetPlayerText(playerResponse);

		RemoveButtonsFromPanel();

		StartCoroutine(CheckResponse());
    }

	private string GetCategory(string prompt) {
		foreach(var c in SpeechPrompts.Categories) {
			if (c.Value.ContainsKey(prompt)){
				return c.Key;
			}
		}

		return "null";
	}

	IEnumerator CheckResponse() {
		yield return new WaitForSeconds(1);

		string cat = GetCategory(prompt);

		if (cat == "null") {
			Debug.Log("Something when horribly wrong.");
		}

		if (SpeechPrompts.Categories[cat][prompt].Contains(playerResponse)) {
			SetNpcText("Good job!");
		} else {
			string reaction = SpeechPrompts.BadReactions[Random.Range(0, SpeechPrompts.BadReactions.Count)];
			SetNpcText(reaction);
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
