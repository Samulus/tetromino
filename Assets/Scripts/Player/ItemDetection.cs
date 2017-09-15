using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetection : MonoBehaviour {

	public bool isInfrontOfItem() {
		Vector3 start = transform.position + transform.TransformDirection(Vector3.forward);
		start.y += 1;
		RaycastHit victim;
		bool infrontOfItem = Physics.Raycast(start, Vector3.down, out victim, 1.0f);
    Debug.DrawRay(start, Vector3.down, infrontOfItem ? Color.red : Color.blue);
		return infrontOfItem;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
