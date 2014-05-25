using UnityEngine;
using System.Collections;

public class Deleteme : MonoBehaviour {

	public GameObject target;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (this.transform.position,
		                                          target.transform.position,
		                                          Time.deltaTime * 1f);
		transform.rotation = Quaternion.RotateTowards(target.transform.rotation, target.transform.rotation, 85.0f * Time.deltaTime);
	}
}
