using UnityEngine;
using System.Collections;

public class BackMusic : MonoBehaviour {
	public AudioSource[] backMusic=new AudioSource[3];
	private GameController gameController;
	private int turn;
	void Awake(){
		turn=(int)(Random.Range (0.0f,30.0f)/10);
		//print ("turn:"+turn);
		backMusic [turn].Play ();
	}
	void Start(){
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		gameController=gameControllerObject.GetComponent<GameController>();
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	void Update(){
		if (gameController.IsOver ())
			backMusic [turn].Pause ();
	}
}
