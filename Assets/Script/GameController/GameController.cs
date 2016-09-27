using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public Transform[] occurEnemyTransform = new Transform[3];
	public GameObject Enemy;
	public Vector2 startWait;
	public Vector2 onceWait;
	public Vector2 waveWait;
	public int waveAccount=2;
	public int onceAccount=2;

	public AudioSource audioVectory;
	public AudioSource audioFailure;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText readyToRestart;

	private bool gameOver;
	private bool restart;
	private int score;
	private int count;
	private bool ArrivedThreeSecond=false;
	void Start()
	{
		count = onceAccount;
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		readyToRestart.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine(OccurEnemy());
		StartCoroutine(WaitForRestart());
	}
	IEnumerator WaitForRestart()
	{
		yield return new WaitForSeconds (1.0f);
		while(true)
		{
			if(restart)
			{
				readyToRestart.text=("Ready:3!");
				yield return new WaitForSeconds (1.0f);
				readyToRestart.text=("Ready:2!");
				yield return new WaitForSeconds (1.0f);
				readyToRestart.text=("Ready:1!");
				yield return new WaitForSeconds (1.0f);
				readyToRestart.text=("Ready Go");
				yield return new WaitForSeconds (1.0f);
				ArrivedThreeSecond=true;
				break;
			}
			yield return new WaitForSeconds (1.2f);
		}
	}
	void Update()
	{
			if (restart)
			{
				//print ("a");
				if (Input.touchCount>=1)
				{
				//print ("he");
					if(ArrivedThreeSecond)
						Application.LoadLevel (Application.loadedLevel);
				}
			}
		if (count == 0) 
		{
			if (gameOver)
			{
				//print("over");
				restartText.text = "Press anyKey for Restart";
				restart = true;
			}
		}
	}
	IEnumerator OccurEnemy()
	{	
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
		while (waveAccount!=0) 
		{
			while(count!=0)
			{
				for(int i=0;i<3;i++)
				{

					Instantiate(Enemy,occurEnemyTransform[i].position,occurEnemyTransform[i].rotation);
				}

				yield return new WaitForSeconds (Random.Range (onceWait.x, onceWait.y));
				count--;
				if (gameOver)
				{
					//print("over");
					restartText.text = "Press anyKey for Restart";
					restart = true;
					break;
				}
			}
			waveAccount--;
			if(waveAccount!=0){
				count=onceAccount;//重新计数为下次循环做准备
				yield return new WaitForSeconds (Random.Range (waveWait.x, waveWait.y));
			}
		}
		while (waveAccount==0) 
		{
			if (GameObject.FindWithTag ("enemy") == null)
			{
				audioVectory.Play();
				gameOverText.text = "You Win!";
				gameOver=true;
				break;
			}
			yield return new WaitForSeconds (1.8f);
		}

	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver ()
	{
		audioFailure.Play ();
		gameOverText.text = "Game Over!";
		gameOver = true;
		//return true;
	}
	public bool IsOver()
	{
		if (gameOver == true)
			return true;
		else 
			return false;
	}
	void OnGUI()
	{
		if(gameOver)
		{
			GUI.Box(new Rect(220,130,150,145),"CHOOSE");
			if(GUI.Button(new Rect(230,160,120,40),"Restart"))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
			if(GUI.Button(new Rect(230,210,120,40),"Exit"))
			{
				Application.Quit ();
			}
		}
	}
}
