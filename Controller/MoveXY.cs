using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Move the whole character
/// </summary>
public class MoveXY : MonoBehaviour {
    public float VerticalSpeed = 6.0f;
    public float TurnSpeed = 900f;
    private float Horizontal;
    private float Vertical;
    private float SideWay;
    private Animator A;
    
	// Use this for initialization
	void Start () {
        Horizontal = 0.0f;
        Vertical = 0.0f;
        SideWay = 0.0f;
        A = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float DisPV = VerticalSpeed * Vertical * Time.deltaTime;
        A.SetFloat("Vertical", Vertical);
        A.SetFloat("Turn", Horizontal);
        A.SetInteger("SideWay", (int)SideWay);
        transform.Translate(SideWay * VerticalSpeed * Time.deltaTime, 0, DisPV, Space.Self);
        transform.Rotate(0, TurnSpeed * Horizontal * Time.deltaTime, 0, Space.Self);
    }

    public void Move(float H, float V, float S)
    {
        Horizontal = H;
        Vertical = V;
        SideWay = S;
    }
}
