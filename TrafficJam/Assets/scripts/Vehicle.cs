using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour {

	//Sounds
	public AudioClip drift;
	public AudioClip explotion;

	//Directions of the current car
	public const int FORWARD = 0;
	public const int TURN_LEFT = 1;
	public const int TURN_RIGHT = 2;

	public const float EXPLODING_ERASE_TIME = 3f;

	public GameScript m;

	public Renderer blue, green, warning;

	//Car type
	public const int CAR_TYPE = 0;
	public const int TRUCK_TYPE = 1;

	//Colors
	public const int RED = 2;
	public const int BLUE = 3;
	public const int GREEN = 4;

	public float speed;
	public int color;
	public bool paused;
	public int channel; //Column that is currently running in
	public int direction;

	Animator animator;

	void ChangeBlue() {
		color = BLUE;
		renderer.enabled = false;
		blue.enabled = true;
		green.enabled = false;
	}
	
	void ChangeGreen() {
		color = GREEN;
		renderer.enabled = false;
		blue.enabled = false;
		green.enabled = true;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "bullet_blue" && color == BLUE) {
			SetStatus(DIE);
		} else if (coll.gameObject.tag == "bullet_red" && color == RED) {
			SetStatus(DIE);
		} else if (coll.gameObject.tag == "bullet_green" && color == GREEN) {
			SetStatus(DIE);
		} else if (coll.gameObject.tag == "car" || coll.gameObject.tag == "car_slow") { 
			if (GetStatus () != EXPLODE) {
				Debug.Log ("OnCollisionEnter2D");
				//m.road.RenderMap (transform.position, renderer.bounds.size.y);
				warningVehicle.ChangeToExploding();
				ChangeToExploding ();
			}
		}
	}

	void ChangeRed() {
		color = RED;
		renderer.enabled = true;
		blue.enabled = false;
		green.enabled = false;
	}

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		switch (color) {
			case RED:
				ChangeRed();
			break;
			case BLUE:
				ChangeBlue();
			break;
			case GREEN:
				ChangeGreen();
			break;
		}
	}

	public const int IDLE = 0;
	public const int WARNING = 1;
	public const int EXPLODE = 2;
	public const int DIE = 3;
	public const int AVOIDING = 4;

	private Vehicle warningVehicle = null;

	public void ChangeToWarning() {
		SetStatus (WARNING);
		warning.renderer.enabled = true;
	}

	public void ChangeToExploding() {
		Debug.Log ("ChangeToExploding");
		audio.PlayOneShot (explotion);
		SetStatus (EXPLODE);
		warning.renderer.enabled = false;
	}

	public void ChangeToIdle() {
		SetStatus (IDLE);
		warning.renderer.enabled = false;
	}

	public class MathUtils {
		public static float YDistance(Vector3 v1, Vector3 v2) {
			float abs1 = v1.y;
			float abs2 = v2.y;
			return Mathf.Abs(abs1 - abs2);
		}

	}

	public float GetWarningDistance() {
		return 1.5f;
	}

	void UpdateIdle(float timeDelta) {
		foreach (Vehicle v in 
		         m.vehicleBag.FindAll(vec => (vec.GetInstanceID() != GetInstanceID()) && (vec.channel == channel) )) {
			if ((v != null) && 
			    (Vector3.Distance(v.transform.position, transform.position)) < GetWarningDistance()) {
				warningVehicle = v;
				ChangeToWarning();
			}
		}
	}

	void UpdateWarning(float timeDelta) {
		if (warningVehicle == null) ChangeToIdle();
		if (warningVehicle.GetStatus() == DIE) ChangeToIdle();
		if (Vector3.Distance(warningVehicle.transform.position, transform.position) > GetWarningDistance() ) {
			ChangeToIdle();
		}
	}

	void UpdateExplode(float timeDelta) {
		SetStatus (EXPLODE);
		explodeTime += timeDelta;
		if (explodeTime >= EXPLODING_ERASE_TIME) {
			SetStatus(DIE);
		}
	}

	public int GetStatus() {
		return animator.GetInteger ("status");
	}

	public void SetStatus(int status) {
		animator.SetInteger("status", status);
	}

	int newColumn = -4;
	float explodeTime = 0f, newAxis = -1f; 
	// Update is called once per frame
	void Update () {
		if (paused)
			return;
		//If to erase the vehicle when goes off from the screen
		if (transform.position.y < GameScript.END_ROAD_Y_AXIS) {
			SetStatus(DIE);
		}

		Quaternion rotation = Quaternion.identity;
		Vector3 waypoint = new Vector3 (this.transform.position.x,
		                                this.transform.position.y + GameScript.END_ROAD_Y_AXIS,
		                                this.transform.position.z);
		switch (GetStatus()) {
		case IDLE:
			UpdateIdle(Time.deltaTime);
			break;
		case WARNING:
			if (warningVehicle.GetStatus() == EXPLODE) {
				//Avoid it
				audio.PlayOneShot (drift);
				SetStatus(AVOIDING);
				if (warningVehicle.channel == GameScript.LEFT_COLUMN) {
					newAxis = GameScript.CENTER_ROW_AXIS;
					newColumn = GameScript.CENTER_COLUMN;
				}
				if (warningVehicle.channel == GameScript.CENTER_COLUMN) {
					newAxis = GameScript.RIGHT_ROW_AXIS;
					newColumn = GameScript.RIGHT_COLUMN;
				}
				if (warningVehicle.channel == GameScript.RIGHT_COLUMN) {
					newAxis = GameScript.CENTER_ROW_AXIS;
					newColumn = GameScript.CENTER_COLUMN;
				}

			}
			UpdateWarning(Time.deltaTime);
			break;
		case AVOIDING:
			waypoint = transform.position;
			if ((waypoint.x == newAxis) && (waypoint.y == (waypoint.y - transform.renderer.bounds.size.y))) {
				ChangeToIdle();
			} else {
				waypoint.x = newAxis;
				channel = newColumn;
				waypoint.y = waypoint.y - transform.renderer.bounds.size.y;
				transform.Rotate(waypoint);
			}
			break;
		case EXPLODE:
			UpdateExplode(Time.deltaTime);
			return;
			break;
		case DIE:
			m.vehicleBag.Remove(this);
			Destroy(gameObject);
			return;
		break;
		default:
			throw new UnityException("Unkown status in the Vehicle animator's.");
			break;
		}

		if ( (GetStatus () != EXPLODE) && (GetStatus () != DIE) ) {
			transform.position = Vector3.MoveTowards (this.transform.position,
			                                          waypoint,
			                                          Time.deltaTime * (speed));
		}
	}
}
