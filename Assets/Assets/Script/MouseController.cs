using UnityEngine;

public class MouseController : MonoBehaviour {

    public GameObject SelectedObject;
    public GameObject HoveredObject;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.Log("Mouse is over: " + hitInfo.collider.name );

            // The collider we hit may not be the "root" of the object
            // You can grab the most "root-est" gameobject using
            // transform.root, though if your objects are nested within
            // a larger parent GameObject (like "All Units") then this might
            // not work.  An alternative is to move up the transform.parent
            // hierarchy until you find something with a particular component.

            var hitObject = hitInfo.transform.root.gameObject;

            SelectObject(hitObject);
        }
        else {
            ClearSelection();
        }

    }

    void SelectObject(GameObject obj)
    {
        if (HoveredObject != null)
        {
            if (obj == HoveredObject)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SelectedObject = HoveredObject;
                    return;
                }
                return;
            }

            ClearSelection();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectedObject = null;
            }
        }

        HoveredObject = obj;
    }

    void ClearSelection()
    {
        HoveredObject = null;
    }
}
