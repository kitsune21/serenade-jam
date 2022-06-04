using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;


public class Typer : MonoBehaviour
{
	// word bank

	public Text wordOutput = null;
	public Text wordCountText = null;
	public Slider slider;
	public float wordCount;
	public float tooFast;
	public float tooSlow;
	public GameObject sliderFill;
	public GameObject tooFastText;
	public GameObject tooSlowText;



	private string remainingWord = string.Empty;
	private string currentWord;
	private bool gamePaused = false;
	


	private void Start()
    {
        SetCurrentWord();
        wordCountText.text = wordCount.ToString();
    }

    private void SetCurrentWord() {
        // get random letters
        currentWord = GenerateRandomCharacters();

        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString) {

	    remainingWord = newString;
	    wordOutput.text = remainingWord;
    }

    private void FixedUpdate() {
	    wordCount -= 0.01f;
	    wordCountText.text = wordCount.ToString();
	    slider.value = wordCount / 10;
	    if (wordCount >= tooFast) {
		    sliderFill.GetComponent<Image>().color = Color.red;
		    tooFastText.SetActive(true);


	    } else if (wordCount <= tooSlow) {
		    sliderFill.GetComponent<Image>().color = Color.red;
		    tooSlowText.SetActive(true);

	    }
	    else {
		    sliderFill.GetComponent<Image>().color = Color.green;
		    tooFastText.SetActive(false);
		    tooSlowText.SetActive(false);
	    }
    }

    private void Update() {
	    if (!pause.GamePaused) {
		    CheckInput();
        }
    }

    private void CheckInput() {
	    if (Input.anyKeyDown) {
		    string keysPressed = Input.inputString;

		    if (keysPressed.Length == 1) {
                EnterLetter(keysPressed);
                
		    }
	    }
    }

    private void EnterLetter(string typedLetter) {
	    if (IsCorrectLetter(typedLetter)) {
            RemoveLetter();

            if (IsWordComplete()) {
	            wordCount++;
                SetCurrentWord();
            }
	    }
    }

    private bool IsCorrectLetter(string letter) {
	    return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter() {
	    string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete() {
	    return remainingWord.Length == 0;
    }

    private String GenerateRandomCharacters() {
	    const string chars = "abcdefghijklmnopqrstuvwxyz";
	    int length = UnityEngine.Random.Range(2,9);

	    var randomWord = new char[length];

	    for (var i = 0; i < length; i++) {
		    randomWord[i] = chars[UnityEngine.Random.Range(0, chars.Length)];
	    }

	    return new string(randomWord);
    }

}

