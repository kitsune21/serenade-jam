using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKnownResponses : MonoBehaviour {
	
    public static List<string> knownSportsResponses = new List<string>() {
	    "Football",
	    "Ah yes, I sports talk",
		"Nay brother. I donteth.",
	    "It was ludicrous"
    };

    public static List<string> knownGeneralResponses = new List<string>() {
	    "Cool story.",
	    "I could go for some pizza.",
	    "Neat."
    };

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
			if (cat.Value.ContainsKey(response)){
				var category = cat.Key;

				if (!knownResponses[category].Contains(response)) {
				knownResponses[category].Add(response);
				}
			}
		}
	}

}

