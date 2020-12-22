using UnityEngine;
using System.Collections;
using UnityEngine.UI; // include UI namespace so can reference UI elements
using UnityEngine.SceneManagement; // include so we can manipulate SceneManager

public class GameManager : MonoBehaviour {

	// static reference to game manager so can be called from other scripts directly (not just through gameobject component)
	public static GameManager gm;

	// levels to move to on victory and lose
	public string levelAfterGameOver = "game over";

	// game performance
	public float score = 0;
	public int highscore = 0;
	public int startHP = 3;
	public int playerHP = 3;

	public float complexity = 0.4f;

	// UI elements to control
	public Text UIScore;
	public Text UIHighScore;
	public GameObject UIGamePaused;

	public Vector3 spawnLocation;

	// private variables
	GameObject _player;

	// set things up here
	void Awake () {
		// setup reference to game manager
		if (gm == null)
			gm = this.GetComponent<GameManager>();

		// setup all the variables, the UI, and provide errors if things not setup properly.
		setupDefaults();
	}

	// game loop
	void Update() {
		// if ESC pressed then pause the game
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (Time.timeScale > 0f) {
				UIGamePaused.SetActive(true); // this brings up the pause UI
				Time.timeScale = 0f; // this pauses the game action
			} else {
				Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)
				UIGamePaused.SetActive(false); // remove the pause UI
			}
		}

		complexity = 0.4f + Time.time / 2000;
		score += complexity * Time.deltaTime * 5;
		if ((int)score > highscore)
			highscore = (int)score;
	}

	// setup all the variables, the UI, and provide errors if things not setup properly.
	void setupDefaults() {
		// setup reference to player
		if (_player == null)
			_player = GameObject.FindGameObjectWithTag("Player");
		
		if (_player==null)
			Debug.LogError("Player not found in Game Manager");

		// get initial _spawnLocation based on initial position of player
		spawnLocation = _player.transform.position;

		highscore = PlayerPrefManager.GetHighscore();

		// friendly error messages
		if (UIScore==null)
			Debug.LogError ("Need to set UIScore on Game Manager.");
		
		if (UIHighScore==null)
			Debug.LogError ("Need to set UIHighScore on Game Manager.");
		
		if (UIGamePaused==null)
			Debug.LogError ("Need to set UIGamePaused on Game Manager.");

		// get the UI ready for the game
		refreshGUI();
	}

	void OnGUI()
    {
		refreshGUI();
	}

	// refresh all the GUI elements
	public void refreshGUI() {
		// set the text elements of the UI
		UIScore.text = "Score: "+ ((int)score).ToString();
		UIHighScore.text = "Highscore: "+ highscore.ToString ();
		
		//refresh hp
	}

	// public function to add points and update the gui and highscore player prefs accordingly
	public void AddPoints(int amount)
	{
		// increase score
		score += amount;
	}

	public void lifeSubstraction(int damage)
	{
		GameManager.gm.playerHP -= damage;
		_player.GetComponent<Player>().playerFire1.SetActive(true);
		_player.GetComponent<Player>().fireAnim.Play("fire1");
		if (playerHP < 2)
		{
			_player.GetComponent<Player>().playerFire2.SetActive(true);
			_player.GetComponent<Player>().fireAnim2.Play("fire1");
		}
		if (playerHP > 1)
		{
			_player.GetComponent<Player>().playerAnimator.Play("player hurt");
			StartCoroutine(_player.GetComponent<Player>().stopAnimationPlayerHurt());
		}
		HealthBar.AdjustCurrentValue(-damage);
	}

	void OnApplicationQuit()
	{
		if (highscore > PlayerPrefManager.GetHighscore())
			PlayerPrefManager.SetHighscore(highscore);		
	}
}
