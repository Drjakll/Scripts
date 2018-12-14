using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelection : MonoBehaviour {

    public Player TargetSelected = null;
    private ActionCaster MB;
	// Use this for initialization
	void Start () {
        MB = GetComponent<ActionCaster>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObject;

            if(Physics.Raycast(ray, out hitObject, 10000.0f))
            {
                if(hitObject.collider.GetComponent<Player>() != null && hitObject.collider.gameObject.name != gameObject.name)
                {
                    TargetSelected = hitObject.collider.GetComponent<Player>();
                    MB.SetTarget(TargetSelected, TargetSelected.GetComponent<CharacterHealth>());
                }
            }
        }
	}
}
