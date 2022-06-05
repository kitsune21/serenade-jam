using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpeechPrompts {

	public static readonly List<string> Exits = new List<string>() {
		"I just remembered I have to go do my business work.",
		"Look! The boss is coming!",
		"I think I forgot something on my desk... Goodbye.",
		"I just remembered I have a hair doctor appointment... Bye.",
		"Oh! Look at the time. I should be going now.",
		"Sorry. I need to go iron my neck tie."
	};

	private static readonly List<string> GenericSports = new List<string>() {
		"They were Sport Balling very hard.",
		"Ah yes. I sports talk.",
		"It was ludicrous.",
		"What were they thinking? Sending in Tommie Irmstrong?"
	};

	private static readonly List<string> YesNo = new List<string>() {
		"Not a chance",
		"Absolutely!",
		"Maybe?",
		"Only if the one thing happens to the one person."
	};

	private static readonly List<string> LudicrousResponses = new List<string>() {
		"What was Wenger thinking sending Walker on that early?"
	};

	private static readonly List<string> LikedSports = new List<string>() {
		"Football",
		"Baseball",
		"Soccer",
		"Golf",
		"Basketball"
	};

	private static readonly List<string> FavTeam = new List<string>() {
		"My favorite team is the Underappreciated Buccaneers."
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

	private static readonly List<string> Dating = new List<string>() {
		"Who is that?",
		"Yes, they are.",
		"They're not."
	};

	private static readonly List<string> Talking = new List<string>() {
		"Why do I care?",
		"Good for him.",
		"I'll fight him!",
		"What a jerk!"
	};

	public static readonly List<string> GeneralResponses = new List<string>(){
		{"Cool story"},
		{"I could go for some pizza."},
		{"Tomatoes make great weapons when water balloons aren't available."},
		{"Neat."},
		{"You're the Bee's Knees!"}
	};

	public static readonly List<string> BadReactions = new List<string>() {
		{"Don't you have something better to do?"},
		{"Ew"},
		{"So.....yeah....I guess"},
		{"Why are you talking to me?"},
		{"......."}
	};

	public static readonly Dictionary<string, List<string>> JokesPrompts = new Dictionary<string, List<string>>() {
		{"What did the Ocean say to the Beach?", new List<string>(){"Nothing, it just waved."}},
		{"Someone walks into a bar.", new List<string>(){"Ouch."}},
		{"What do you call a bagel that can fly?", new List<string>(){"A plane bagel"}},
		{"What do you call a boomerang that doesn't come back?", new List<string>(){"A stick"}},
		{"What does a Triangle call a Circle?", new List<string>(){"Pointless"}},
		{"What do you call a funny mountain?", new List<string>(){"Hill-arious"}}
	};

	public static readonly Dictionary<string, List<string>> GossipPrompts = new Dictionary<string, List<string>>() {
		{"Do you know if Brian is dating anyone?", Dating},
		{"Do you know if Betty is dating anyone?", Dating},
		{"I think I heard Paul talking about you.", Talking},
		{"I heard Steven is stealing Protein Powder from the Break Room.", Talking}
	};

	public static readonly Dictionary<string, List<string>> SportsPrompts = new Dictionary<string, List<string>>() {
		{"Did you see that ludicrous display last night?", LudicrousResponses},
		{"Did you catch the game last night?", GenericSports},
		{"What's your favorite sports team?", FavTeam},
		{"What sports do you like?", LikedSports},
		{"Did you see Steve McDichael's ludicrous display last night?", LudicrousResponses},
		{"Do you think the \"Chubby Horses\" will win tonight?", YesNo},
		{"Bro, do you even lift?", Lift},
		{"Brother! Doth thou even hoist?", Hoist}
	};

	public static readonly Dictionary<string, List<string>> GeneralPrompts = new Dictionary<string, List<string>>() {
		{"null", GeneralResponses}
	};

	public static readonly Dictionary<string, Dictionary<string, List<string>> > Categories = new Dictionary<string, Dictionary<string, List<string>> > {
		{"Sports", SportsPrompts},
		{"Gossip", GossipPrompts},
		{"Jokes", JokesPrompts},
		{"General", GeneralPrompts}
	};

}
