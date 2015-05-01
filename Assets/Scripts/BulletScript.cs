using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	// Destroy bullet once it's off screen
	void OnBecameInvisible () {
		DestroyObject (gameObject);
	}
}
