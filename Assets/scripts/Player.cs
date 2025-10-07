using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
	public float ballProximity = 4f;
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
	public bool canPick = false;

	public bool picked = false;
	public bool won = false;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (canPick == true) {
<<<<<<< Updated upstream
<<<<<<< Updated upstream

			if (/*GvrViewer.Instance.Triggered ||*/ Input.GetKeyDown ("space")) {
				RaycastHit hit;

				if (Physics.Raycast(transform.position, transform.forward, out hit)) {

					Cup cup = hit.transform.GetComponent<Cup> ();
					if (cup != null) {
						canPick = false;

						picked = true;
						won = (cup.ball != null);

						cup.MoveUp ();
					}

<<<<<<< Updated upstream
		if (Physics.Raycast(transform.position, transform.forward, out hit)) {
			if (hit.transform.GetComponent<Ball>() != null) {
				Ball ball = hit.transform.GetComponent<Ball> ();

				if (ball.transform.position.z - transform.position.z < ballProximity && ball.direction.z < 0) {
					ball.OnPlayerHit ();
=======
>>>>>>> Stashed changes
=======

			if (/*GvrViewer.Instance.Triggered ||*/ Input.GetKeyDown ("space")) {
				RaycastHit hit;

				if (Physics.Raycast(transform.position, transform.forward, out hit)) {

					Cup cup = hit.transform.GetComponent<Cup> ();
					if (cup != null) {
						canPick = false;

						picked = true;
						won = (cup.ball != null);

						cup.MoveUp ();
					}

>>>>>>> Stashed changes
=======

			if (/*GvrViewer.Instance.Triggered ||*/ Input.GetKeyDown ("space")) {
				RaycastHit hit;

				if (Physics.Raycast(transform.position, transform.forward, out hit)) {

					Cup cup = hit.transform.GetComponent<Cup> ();
					if (cup != null) {
						canPick = false;

						picked = true;
						won = (cup.ball != null);

						cup.MoveUp ();
					}

>>>>>>> Stashed changes
				}
			}

		}
	}
}
