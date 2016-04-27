using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChestScript : MonoBehaviour {
    public Class.inventory Inventory;

    // Use this for initialization
    void Start () {
        Inventory = new Class.inventory(400);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
