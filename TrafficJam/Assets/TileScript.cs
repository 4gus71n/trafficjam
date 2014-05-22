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

	public void ChangeFullColor(int color) {

	}

	public void SetMixColor(int color, bool lookingLeft) {
		
	}


	public float GetRightX() {
		return transform.position.x + (renderer.bounds.size.x / 2);
	}

	public float GetLeftX() {
		return transform.position.x - (renderer.bounds.size.x / 2);
	}

	public float GetTopY() {
		return transform.position.y + (renderer.bounds.size.y / 2);
	}

	public float GetBottomY() {
		return transform.position.y - (renderer.bounds.size.y / 2);
	}
}
