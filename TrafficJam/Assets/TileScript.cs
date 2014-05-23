using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

	public const int LIGHT_GREY = 0;
	public const int GREY = 1;
	public const int RED = 2;
	public const int BLUE = 3;
	public const int GREEN = 4;
	
	public int row; //1,2,3,4,5,6,7...15
	public int column; //1,2,3
	public int color;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

			
	}

	public void ChangeFullColor(int ncolor) {
		foreach (Renderer r in GetComponents<Renderer>()) {
			r.enabled = false;
		}
		color = ncolor;
		switch (color) {
		case GREY:
			transform.Find("grey_tile").renderer.enabled = true;
			break;
		case LIGHT_GREY:
			transform.Find("light_grey_tile").renderer.enabled = true;
			break;
		case RED:
			transform.Find("red_tile").renderer.enabled = true;
			break;
		case BLUE:
			transform.Find("blue_tile").renderer.enabled = true;
			break;
		case GREEN:
			transform.Find("green_tile").renderer.enabled = true;
			break;
		}
	}

	public void SetMixColor(int ncolor, bool lookingLeft) {
		foreach (Renderer r in GetComponents<Renderer>()) {
			r.enabled = false;
		}
		string p1 = "", p2 = "";
		switch (color) {
		case GREY:
			p1 = "grey_d_";
			break;
		case LIGHT_GREY:
			p1 = "light_grey_d_";
			break;
		case RED:
			p1 = "red_d_";
			break;
		case BLUE:
			p1 = "blue_d_";
			break;
		case GREEN:
			p1 = "green_d_";
			break;
		}
		switch (ncolor) {
		case GREY:
			p2 = "grey_left";
			break;
		case LIGHT_GREY:
			p2 = "light_grey_left";
			break;
		case RED:
			p2 = "red_left";
			break;
		case BLUE:
			p2 = "blue_left";
			break;
		case GREEN:
			p2 = "green_left";
			break;
		}
		Debug.Log (p1 + p2);
		transform.Find(p1+p2).renderer.enabled = true;
	}


	public float GetRightX() {
		return renderer.bounds.max.x;
	}

	public float GetLeftX() {
		return renderer.bounds.min.x;
	}

	public float GetTopY() {
		return renderer.bounds.max.y;
	}

	public float GetBottomY() {
		return renderer.bounds.min.y;
	}
}
