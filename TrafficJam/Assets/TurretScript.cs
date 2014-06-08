using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {

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
	public bool isMoving;
	public GameScript m;

	void OnMouseDrag(){
		isMoving = true;
	}

	public void ChangeBlue() {
		color = Vehicle.BLUE;
		redrenderer.enabled = false;
		bluerenderer.enabled = true;
		greenrenderer.enabled = false;
	}

	public void ChangeGreen() {
		color = Vehicle.GREEN;
		redrenderer.enabled = false;
		bluerenderer.enabled = false;
		greenrenderer.enabled = true;
	}
	
	public void ChangeRed() {
		color = Vehicle.RED;
		redrenderer.enabled = true;
		bluerenderer.enabled = false;
		greenrenderer.enabled = false;
	}

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
	}

	void OnMouseUp() {
		isMoving = false;
		SetState (TURRET_STATE_IDLE);
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving) {
			Vector3 mouse = Input.mousePosition;
			Vector3 vec = Camera.main.ScreenToWorldPoint (mouse);
			float x = vec.x;
			transform.position = new Vector3(x, transform.position.y, 0f);
		}
	}

	void OnMouseDown() {
		SetState(TURRET_STATE_SHOOTING);
	}

	void ShootBullet() {
		SetState(TURRET_STATE_IDLE);
		Vector3 origin = new Vector3 (transform.position.x, transform.position.y + 1f, 0f);
		BulletScript copy = null;

		switch (color) {
		case Vehicle.RED:
			copy = Instantiate (redbullet, origin, Quaternion.identity) as BulletScript;
			break;
		case Vehicle.BLUE:
			copy = Instantiate (bluebullet, origin, Quaternion.identity) as BulletScript;
			break;
		case Vehicle.GREEN:
			copy = Instantiate (greenbullet, origin, Quaternion.identity) as BulletScript;
			break;
		}
		copy.SetChannel (channel);
		copy.destiny = new Vector3 (origin.x, origin.y + 8f, 0f);
		copy.SetColor (color);
		copy.triggered = true;
	}

	public void SetState(int state) {
		animator.SetInteger ("state", state);
	}

}
