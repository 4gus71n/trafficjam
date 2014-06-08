using UnityEngine;
using System.Collections;

public class HappyGuiScript : MonoBehaviour {

	public TurretScript turret;
	public SpriteRenderer redrenderer;
	public SpriteRenderer bluerenderer;
	public SpriteRenderer greenrenderer;
	// Use this for initialization
	void Start () {
		switch (turret.color) {
		case Vehicle.RED:
			turret.ChangeRed();
			ChangeRed();
			break;
		case Vehicle.BLUE:
			ChangeBlue();
			turret.ChangeBlue();
			break;
		case Vehicle.GREEN:
			ChangeGreen();
			turret.ChangeGreen();
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeBlue() {
		redrenderer.enabled = false;
		bluerenderer.enabled = true;
		greenrenderer.enabled = false;
	}
	
	public void ChangeGreen() {
		redrenderer.enabled = false;
		bluerenderer.enabled = false;
		greenrenderer.enabled = true;
	}
	
	public void ChangeRed() {
		redrenderer.enabled = true;
		bluerenderer.enabled = false;
		greenrenderer.enabled = false;
	}

	void OnMouseDown() {
		switch (turret.color) {
		case Vehicle.RED:
			turret.color = Vehicle.BLUE;
			turret.ChangeBlue();
			ChangeBlue();
			break;
		case Vehicle.BLUE:
			turret.color = Vehicle.GREEN;
			turret.ChangeGreen();
			ChangeGreen();
			break;
		case Vehicle.GREEN:
			turret.color = Vehicle.RED;
			turret.ChangeRed();
			ChangeRed();
			break;
		}
	}
}
