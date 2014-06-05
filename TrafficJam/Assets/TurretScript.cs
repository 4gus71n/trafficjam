using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {

	public const int TURRET_RED = 0;
	public const int TURRET_BLUE = 1;
	public const int TURRET_GREEN = 2;

	public const int TURRET_STATE_IDLE = 0;
	public const int TURRET_STATE_SHOOTING = 1;

	public Renderer redrenderer;
	public Renderer bluerenderer;
	public Renderer greenrenderer;
	public Animator animator;

	public BulletScript redbullet;
	public BulletScript bluebullet;
	public BulletScript greenbullet;

	public int channel;
	public int color;

	void ChangeBlue() {
		color = TURRET_BLUE;
		redrenderer.enabled = false;
		bluerenderer.enabled = true;
		greenrenderer.enabled = false;
	}

	void ChangeGreen() {
		color = TURRET_GREEN;
		redrenderer.enabled = false;
		bluerenderer.enabled = false;
		greenrenderer.enabled = true;
	}
	
	void ChangeRed() {
		color = TURRET_RED;
		redrenderer.enabled = true;
		bluerenderer.enabled = false;
		greenrenderer.enabled = false;
	}

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		switch (color) {
		case TURRET_RED:
			ChangeRed();
			break;
		case TURRET_BLUE:
			ChangeBlue();
			break;
		case TURRET_GREEN:
			ChangeGreen();
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		switch (color) {
		case TURRET_RED:
			color = TURRET_BLUE;
			ChangeBlue();
			break;
		case TURRET_BLUE:
			color = TURRET_GREEN;
			ChangeGreen();
			break;
		case TURRET_GREEN:
			color = TURRET_RED;
			ChangeRed();
			break;
		}
		SetState(TURRET_STATE_SHOOTING);
	}

	void ShootBullet() {
		SetState(TURRET_STATE_IDLE);
		Vector3 origin = new Vector3 (transform.position.x, transform.position.y + 1f, 0f);
		BulletScript copy = null;

		switch (color) {
		case TURRET_RED:
			copy = Instantiate (redbullet, origin, Quaternion.identity) as BulletScript;
			break;
		case TURRET_BLUE:
			copy = Instantiate (bluebullet, origin, Quaternion.identity) as BulletScript;
			break;
		case TURRET_GREEN:
			copy = Instantiate (greenbullet, origin, Quaternion.identity) as BulletScript;
			break;
		}
		copy.SetChannel (channel);
		copy.SetColor (color);
		copy.triggered = true;
	}

	public void SetState(int state) {
		animator.SetInteger ("state", state);
	}

}
