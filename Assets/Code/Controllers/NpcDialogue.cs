using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialogue : MonoBehaviour
{
    public Text NpcText;
	public Text PlayerText;
	public GameObject TextPanel;
	public GameObject PromptPanel;
    public GameObject TestingPanel;
	public Button PromptButton;
	public Button CategoryButton;
	public Button BackButton;
	public PlayerKnownDialogue Player;
	private List<Button> PanelButtons = new List<Button>();
	private string Response;
	private string PlayerPrompt;
    
    [SerializeField]
    public Dictionary<string, List<string>> prompts = new Dictionary<string, List<string>>();

	private void CreatePrompts() {
		var categories = new List<string>();

		foreach(var c in SpeechPrompts.Categories.Keys) {
			categories.Add(c);
		}

        var pickedCats = new List<string>();

        for(var i = 0; i < 2; i++) {
            var cat = categories[Random.Range(0, categories.Count)];
            if (!pickedCats.Contains(cat)) {

                if (cat == "General") {
                    i--;
                    continue;
                }
                pickedCats.Add(cat);

                var NpcChoices = new List<string>();
                var chosenCat = SpeechPrompts.Categories[cat];

                foreach(var p in chosenCat.Keys){
                    NpcChoices.Add(p);
                }

                int count = NpcChoices.Count;

                for (var j = 0; j < count / 2; j++) {
                    var prompt = NpcChoices[Random.Range(0, NpcChoices.Count)];
                    prompts.Add(prompt, chosenCat[prompt]);
                    NpcChoices.Remove(prompt);
                }
            }
        }

		
	}

    public void OpenNpcDialogue() {
        TextPanel.SetActive(true);
        PromptPanel.SetActive(true);
        TestingPanel.SetActive(false);
        SetNpcText("Hello");
        SetPlayerText("");
        InstantiateButtons(GetPromptCategories(), CategoryButton);
    }

    void Start()
    {
        TextPanel.SetActive(false);
        PromptPanel.SetActive(false);
        Player.LearnResponse("Cool story");
        Player.LearnResponse("Neat.");
        CreatePrompts();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Close();
        }
    }

    private void Close() {
        RemoveButtonsFromPanel();
        TextPanel.SetActive(false);
        PromptPanel.SetActive(false);
        TestingPanel.SetActive(true);
    }

    private void SetNpcText(string text) {
	    NpcText.text = text;
		Player.LearnResponse(text);
    }

    private void SetPlayerText(string text) {
	    PlayerText.text = text;
    }

    private IEnumerator NpcRespond(string prompt) {
        List<string> npcResponses = new List<string>();
        foreach (var s in prompts.Keys) {
		    npcResponses.Add(s);
	    }

        yield return new WaitForSeconds(1);

        if (!npcResponses.Contains(prompt)) {
            SetNpcText("I don't know what you're talking about.");
        } else {
            int responseIndex = Random.Range(0, prompts[prompt].Count);
            string response = prompts[prompt][responseIndex];
            SetNpcText(response);
            Player.LearnResponse(response);
        }

        yield return new WaitForSeconds(2);

        InstantiateButtons(GetPromptCategories(), CategoryButton);
    }

    private List<string> GetPromptCategories() {
	    var promptCategories = new List<string>();
	    foreach (var c in Player.knownPrompts.Keys) {
		    promptCategories.Add(c);
	    }

	    return promptCategories;
    }

    private void InstantiateButtons(List<string> prompts, Button buttonType) {
	    foreach (var r in prompts) {
		    Button prompt = Instantiate(buttonType, PromptPanel.transform) as Button;
		    prompt.GetComponentInChildren<Text>().text = r;
		    prompt.GetComponent<ButtonScript>().ButtonText = r;
            prompt.GetComponent<ButtonScript>().SpeechType = "prompt";
		    PanelButtons.Add(prompt);
	    }

	    if (buttonType == PromptButton) {
		    Button back = Instantiate(BackButton, PromptPanel.transform) as Button;
		    back.GetComponentInChildren<Text>().text = "Back";
            back.GetComponent<ButtonScript>().SpeechType = "prompt";
		    PanelButtons.Add(back);
	    }
    }

    public void OpenCategory(GameObject category) {
	    string cat = category.GetComponent<ButtonScript>().ButtonText;

	    RemoveButtonsFromPanel();

	    InstantiateButtons(Player.knownPrompts[cat], PromptButton);
    }

    private void RemoveButtonsFromPanel() {
	    foreach (Transform child in PromptPanel.transform) {
		    GameObject.Destroy(child.gameObject);
	    }
    }

    public void HandleBack() {
		RemoveButtonsFromPanel();
		InstantiateButtons(GetPromptCategories(), CategoryButton);
    }

    public void MakePrompt(GameObject prompt) {
        PlayerPrompt = prompt.GetComponent<ButtonScript>().ButtonText;

        SetNpcText("");
        SetPlayerText(PlayerPrompt);

        RemoveButtonsFromPanel();

        StartCoroutine(NpcRespond(PlayerPrompt));
    }

}
