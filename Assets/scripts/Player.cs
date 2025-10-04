using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float ballProximity = 4f;
	public Animator animator;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.forward, out hit)) {
			if (hit.transform.GetComponent<Ball>() != null) {
				Ball ball = hit.transform.GetComponent<Ball> ();
                if (ball.transform.position.z - transform.position.z < ballProximity+2 && ball.direction.z < 0)
				{
                    animator.ResetTrigger("Hit");
                    animator.SetTrigger("Hit");
                }
                    if (ball.transform.position.z - transform.position.z < ballProximity && ball.direction.z < 0) {
					ball.OnPlayerHit ();
				}

			}
		}
	}
}
