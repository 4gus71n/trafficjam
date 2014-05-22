using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour {

	public GameScript m;

	public Renderer blue, green, warning;

	public const int CAR_TYPE = 0;
	public const int TRUCK_TYPE = 1;

	public const int RED = 0;
	public const int BLUE = 1;
	public const int GREEN = 2;

	public float speed;
	public int color;
	public bool paused;
	public int channel;

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

	void OnCollisionEnter2D() {
		m.RenderMap (transform.position, renderer.bounds.size.y);
		ChangeToExploding ();
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

	private Vehicle warningVehicle = null;

	public float GetWarningDistance() {
		return renderer.bounds.size.y;
	}

	public void ChangeToWarning() {
		SetStatus (WARNING);
		warning.renderer.enabled = true;
	}

	public void ChangeToExploding() {
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

	void UpdateIdle(float timeDelta) {
		Vehicle min = null;
		foreach (Vehicle another in m.vehicleBag.FindAll(vq => vq.gameObject.GetInstanceID() != gameObject.GetInstanceID() )) {
			if (((MathUtils.YDistance(another.transform.position, transform.position) <= (GetWarningDistance())*2))) {
				min = another;
			}
		}
		if (min != null) {
			if (min.GetStatus() == EXPLODE) {
				Vector3 target = min.transform.position;
				target.x += 2f; //The operation may change!!!!
				target.y -= -2f;
				transform.rotation = Quaternion.LookRotation (Vector3.forward,target - transform.position); // we get the angle has to be rotated
				
				transform.position = Vector3.MoveTowards (this.transform.position,
				                                          target,
				                                          timeDelta * speed);	


				
			} else {
				warningVehicle = min;
				ChangeToWarning();
			}
		}
	}

	void UpdateWarning(float timeDelta) {
		if ((MathUtils.YDistance(warningVehicle.transform.position, transform.position)) > GetWarningDistance() ) {
			warningVehicle.ChangeToIdle();
			ChangeToIdle();
		}
	}

	void UpdateExplode(float timeDelta) {
		ChangeToExploding ();
	}

	public int GetStatus() {
		return animator.GetInteger ("status");
	}

	public void SetStatus(int status) {
		animator.SetInteger("status", status);
	}

	// Update is called once per frame
	void Update () {
		if (paused)
			return;
		//If to erase the vehicle when goes off from the screen
		if (transform.position.y < GameScript.END_ROAD_Y_AXIS) {
			m.vehicleBag.Remove(this);
			Destroy(gameObject);
		}
		switch (GetStatus()) {
		case IDLE:
			UpdateIdle(Time.deltaTime);
			break;
		case WARNING:
			UpdateWarning(Time.deltaTime);
			break;
		case EXPLODE:
			UpdateExplode(Time.deltaTime);
			return;
			break;
		default:
			throw new UnityException("Unkown status in the Vehicle animator's.");
			break;
		}

		//Detect if theres any warning aroudn
		transform.position = Vector3.MoveTowards (this.transform.position,
			                     new Vector3 (this.transform.position.x,
			            					  this.transform.position.y + GameScript.END_ROAD_Y_AXIS,
			             					  this.transform.position.z),
			Time.deltaTime * speed);	
	}
}
