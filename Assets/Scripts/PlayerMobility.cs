using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMobility : MonoBehaviour {

	public float speed;
	public float bulletSpeed;
	public GameObject bulletPrefab;
	public Joystick joystick;
	public Vector3 startPos;
	
	void Start () {
		startPos = joystick.transform.position;
	}

	// Rotate player in sync with joystick and move in that direction
	void FixedUpdate () {
		Vector3 diff = joystick.transform.position - startPos;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

		if (Mathf.Abs (diff.x) > 0.1 || Mathf.Abs (diff.y) > 0.1) {
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed);
		}
	}

	// Fire bullet in direction of player
	public void FireBullet () {
		GameObject Clone = (Instantiate(
			bulletPrefab, transform.position,transform.rotation)) as GameObject;

		Clone.GetComponent<Rigidbody2D> ().AddForce (transform.up * bulletSpeed);
	}
}
