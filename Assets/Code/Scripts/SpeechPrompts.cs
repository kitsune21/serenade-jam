using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpeechPrompts {

	private static readonly List<string> GenericSports = new List<string>() {
		"They were Sport Balling very hard.",
		"Ah yes. I sports talk.",
		"It was ludicrous.",
		"What were they thinking? Sending in Tommie Irmstrong?"
	};

	private static readonly List<string> LudicrousResponses = new List<string>() {
		"What was Wenger thinking sending Walker on that early?"
	};

	private static readonly List<string> LikedSports = new List<string>() {
		"Football"
	};

	private static readonly List<string> FavTeam = new List<string>() {
		"My favorite team is the “Underappreciated Buccaneers”."
	};

	private static readonly List<string> Hoist = new List<string>() {
		"Nay brother. I donteth.",
		"Yea brother!",
		"What?"
	};

	private static readonly List<string> Lift = new List<string>() {
		"No sir, I do not.",
		"Yes I do!",
		"Huh?"
	};

	public static readonly Dictionary<string, List<string>> SportsPrompts = new Dictionary<string, List<string>>() {
		{"Did you see that ludicrous display last night? ", LudicrousResponses},
		{"Did you catch the game last night?", GenericSports},
		{"What's your favorite sports team?", FavTeam},
		{"What sports do you like?", LikedSports},
		{"Did you see Steve McDichael's ludicrous display last night?", LudicrousResponses},
		{"Do you think the \"Chubby Horses\" will win tonight?", GenericSports},
		{"Bro, do you even lift?", Lift},
		{"Brother! Doth thou even hoist?", Hoist}
	};
}
