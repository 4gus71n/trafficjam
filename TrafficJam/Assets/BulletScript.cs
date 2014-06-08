using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public int color;
	public int channel;
	public int speed = 3;
	public Vector3 destiny;
	public bool triggered;
	
	// Use this for initialization
	void Start () {
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "car") {
			Vehicle v = collision.gameObject.GetComponent<Vehicle>();
			if (v.color == color) {
				Debug.Log ("Sarasassss");
				Destroy(this.gameObject);
			} else {
				destiny = Vector3.Reflect (destiny, collision.contacts [0].normal);
			}
		}
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

	public void SetChannel(int c) {
		channel = c;
	}

}
