using UnityEngine;

public class SelectionIndicator : MonoBehaviour {

    private MouseController _mm;
    private GameObject _selector;
    private TextMesh _textName;

	// Use this for initialization
	void Start ()
    {
        _selector = GameObject.Find("Image Selection Indicator");
        _textName = transform.GetComponentInChildren<TextMesh>();
        _mm = FindObjectOfType<MouseController>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (_mm.SelectedObject == null) return;
	    if (_mm.SelectedObject.gameObject.name == "World")
	    {
	        _selector.GetComponent<Renderer>().enabled = false;
	        _textName.GetComponent<Renderer>().enabled = false;
	        return;
	    }
	    _selector.GetComponent<Renderer>().enabled = true;
	    _textName.GetComponent<Renderer>().enabled = true;
	    _textName.text = _mm.SelectedObject.transform.name;
	    transform.position = new Vector3(_mm.SelectedObject.transform.position.x, 0.001f, _mm.SelectedObject.transform.position.z);
	    _selector.transform.localScale = new Vector3(_mm.SelectedObject.transform.localScale.x / 7, 1, _mm.SelectedObject.transform.localScale.z / 7);
	}
}
