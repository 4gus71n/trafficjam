using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public int color;
	public int channel;
	public int speed = 3;
	private Vector3 destiny;
	public bool triggered;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (triggered) {
			transform.position = Vector3.MoveTowards (this.transform.position,
			                     destiny,
			                     Time.deltaTime * (speed));
		}
	}

	public void SetColor(int c) {
		color = c;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "bullet_blue" && color == BLUE) {
			SetStatus(DIE);
		}
	}

	public void SetChannel(int c) {
		switch (c) {
		case 0:
			destiny = new Vector3(GameScript.LEFT_ROW_AXIS, 5, 0f);
			break;
		case 1:
			destiny = new Vector3(GameScript.CENTER_ROW_AXIS, 5, 0f);
			break;
		case 2:
			destiny = new Vector3( GameScript.RIGHT_ROW_AXIS, 5, 0f);
			break;
		}
		channel = c;
	}

}
