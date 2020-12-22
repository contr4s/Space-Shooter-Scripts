using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // include so we can manipulate SceneManager

public static class PlayerPrefManager {

	public static int GetScore() {
		if (PlayerPrefs.HasKey("Score")) {
			return PlayerPrefs.GetInt("Score");
		} else {
			return 0;
		}
	}

	public static void SetScore(int score) {
		PlayerPrefs.SetInt("Score",score);
	}

	public static int GetHighscore() {
		if (PlayerPrefs.HasKey("Highscore")) {
			return PlayerPrefs.GetInt("Highscore");
		} else {
			return 0;
		}
	}

	public static void SetHighscore(int highscore) {
		PlayerPrefs.SetInt("Highscore",highscore);
	}


	// story the current player state info into PlayerPrefs
	public static void SavePlayerState(int score, int highScore) {
		// save currentscore and lives to PlayerPrefs for moving to next level
		PlayerPrefs.SetInt("Score",score);
		PlayerPrefs.SetInt("Highscore",highScore);
	}
	
	// output the defined Player Prefs to the console
	public static void ShowPlayerPrefs() {
		// store the PlayerPref keys to output to the console
		string[] values = {"Score","Highscore"};

		// loop over the values and output to the console
		foreach(string value in values) {
			if (PlayerPrefs.HasKey(value)) {
				Debug.Log (value+" = " + PlayerPrefs.GetInt(value));
			} else {
				Debug.Log (value+" is not set.");
			}
		}
	}
}
