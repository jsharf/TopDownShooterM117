﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class PlayerMobility : MonoBehaviour {

	public float speed;
	public GameObject bulletPrefab;
	public GameObject joystick;
	public Vector3 startPos;
	public GameObject jumpButton;

	void Start () {
		DontDestroyOnLoad (this);
		joystick = GameObject.Find ("MobileJoystick");
		jumpButton = GameObject.Find ("JumpButton");
		startPos = joystick.transform.position;

		EventTrigger eventTrigger = jumpButton.GetComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback = new EventTrigger.TriggerEvent();
		UnityEngine.Events.UnityAction<BaseEventData> callback =
			new UnityEngine.Events.UnityAction<BaseEventData>(FireBullet);
		entry.callback.AddListener(callback);
		eventTrigger.delegates.Add(entry);
	}

	// Rotate player in sync with joystick and move in that direction
	void FixedUpdate () {
		Vector3 diff = joystick.transform.position - startPos;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

		if ((Mathf.Abs (diff.x) > 0.1 || Mathf.Abs (diff.y) > 0.1) && GetComponent<NetworkView>().isMine) {
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed);
		}
	}

	// Fire bullet in direction of player
	public void FireBullet(UnityEngine.EventSystems.BaseEventData baseEvent) {
		if (GetComponent<NetworkView> ().isMine) {
			GameObject bullet = (GameObject) Network.Instantiate (
				bulletPrefab, transform.position, transform.rotation, 0);
		}
	}
}
