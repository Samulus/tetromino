using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	private ItemDetection _itemDetection;
	private GameObject heldItem;
	
	void Start () {
		_itemDetection = GetComponent<ItemDetection>();
	}
}
