using UnityEngine;
using System.Collections;

public class MineralScript : MonoBehaviour {
    
    private string _loot { get; set; }

    public Class.Mineral Mine()
    {
        return new Class.Mineral(_loot, Random.Range(12, 14));
    }

    // Use this for initialization
    void Start () {
        _loot = transform.name.ToLower();
        //print(toString());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private string toString()
    {
        return (transform.name + " loot " + _loot + " position " + transform.position);
    }
}
