using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicked : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
		
	}
}
