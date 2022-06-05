using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKnownDialogue : MonoBehaviour {
	
    public static List<string> knownSportsResponses = new List<string>();

    public static List<string> knownGeneralResponses = new List<string>();

	public static List<string> knownGossipResponses = new List<string>();

	public static List<string> knownJokesResponses = new List<string>();

	public static List<string> knownGeneralPrompts = new List<string>();

	public static List<string> knownGossipPrompts = new List<string>();
	
	public static List<string> knownSportsPrompts = new List<string>();

	public static List<string> knownJokesPrompts = new List<string>();

    public readonly Dictionary<string, List<string>> knownResponses = new Dictionary<string, List<string>>() {
	    {"Sports", knownSportsResponses},
	    {"General", knownGeneralResponses},
		{"Gossip", knownGossipResponses},
		{"Jokes", knownJokesResponses}
    };

	public readonly Dictionary<string, List<string>> knownPrompts = new Dictionary<string, List<string>>() {
		{"General", knownGeneralPrompts},
		{"Sports", knownSportsPrompts},
		{"Gossip", knownGossipPrompts},
		{"Jokes", knownJokesPrompts}
	
	};

	public void LearnPrompt(string prompt) {
		
		foreach(var cat in SpeechPrompts.Categories) {
			if (cat.Value.ContainsKey(prompt)){
				var category = cat.Key;

				if (!knownPrompts[category].Contains(prompt)) {
					knownPrompts[category].Add(prompt);
				}
			}
		}
	}

	public void LearnResponse(string response) {
		foreach(var cat in SpeechPrompts.Categories) {
			var category = cat.Key;
			foreach(var prompt in cat.Value) {
				if (prompt.Value.Contains(response)){
					if (!knownResponses[category].Contains(response)) {
						knownResponses[category].Add(response);
					}
				}
			}
		}
	}

}

