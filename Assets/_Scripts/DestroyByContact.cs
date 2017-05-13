using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public float lifetime;
	public int scoreValue;
	private GameController gameController;
	public GameObject Explosion;
	
	void Start ()
	{
		Destroy (gameObject, lifetime);
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void Update()
	{	
		if(transform.childCount>0)
		{
			gameController.AddScore(scoreValue);
			Instantiate(Explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}