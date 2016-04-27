using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
################################
#          A FAIRE             #
################################
- créer un moyen d'instancier des
builders et de les faire reconnaitre
comme alliés
*/

public class WorldController : MonoBehaviour {

    public GameObject Builder;
    public GameObject Iron;
    public GameObject Tree;
    public GameObject Chest;
    private Object _b1;
    private Object _b2;
    //private List<> builderList;

    // Use this for initialization
    void Start () {
        _b1 = Instantiate(Builder, new Vector3(4, 0.75f, 5), Quaternion.identity);
        _b1.name = "Builder 1";
        _b2 = Instantiate(Builder, new Vector3(6, 0.75f, 5), Quaternion.identity);
        _b2.name = "Builder 2";
    }

    // Update is called once per frame
    void Update () {
	
	}
}
