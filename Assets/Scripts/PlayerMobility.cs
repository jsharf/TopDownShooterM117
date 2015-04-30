using UnityEngine;
using System.Collections;

public class PlayerMobility : MonoBehaviour {

	public float speed;
	public float bulletSpeed;
	public GameObject bulletPrefab;

	void FixedUpdate () {
		var rigidbody2d = GetComponent<Rigidbody2D> ();

		// Rotate towards mouse position
		Vector3 position = GetTouchPosition ();
		Quaternion rotation = Quaternion.LookRotation (transform.position - position,
		                                               Vector3.forward);

		transform.rotation = rotation; // set rotation of ship, rotation required to look at mouse
		transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z); // strip x and y rot
		rigidbody2d.angularVelocity = 0; // prevent sliding
	
		// Move towards mouse if its far away enough
		var xdiff = Mathf.Abs (position.x - rigidbody2d.position.x);
		var ydiff = Mathf.Abs (position.y - rigidbody2d.position.y);
		if (xdiff > 1 || ydiff > 1) {
			rigidbody2d.AddForce (gameObject.transform.up * speed); // y
		}

		// Fire bullet on spacebar
		if (Input.GetKeyDown ("space")) {
			FireBullet ();
		}
	}

	public void FireBullet () {
		//Clone of the bullet
		GameObject Clone;
		
		//spawning the bullet at position
		Clone = (Instantiate(bulletPrefab, transform.position,transform.rotation)) as GameObject;

		//add force to the spawned objected
		Clone.GetComponent<Rigidbody2D> ().AddForce (transform.up * bulletSpeed);
	}

	Vector3 GetTouchPosition () {

		Vector3 position;

		if (Application.platform == RuntimePlatform.Android) {
			position = Input.GetTouch (0).position;
		} else if (Application.platform == RuntimePlatform.OSXEditor) {
			position = Input.mousePosition;
		} else {
			position = new Vector3 (0, 0, 0);
		}

		return Camera.main.ScreenToWorldPoint (position);
	}
}
