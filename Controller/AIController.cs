using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Player {
    public Player Target;
    // Use this for initialization
    void Start () {
        MB = GetComponent<ActionCaster>();
        MXY = GetComponent<MoveXY>();
        MB.SetTarget(Target, Target.GetComponent<CharacterHealth>());
    }
	
	// Update is called once per frame
	void Update () {

        if (!CurrentStatus())
            return;

        if (Input.GetKey(KeyCode.F6))
        {
            //Guard
            MB.GiveCommand(Block);
            MXY.Move(0, 0, 0);
            return;
        }
        //Attacks
        else if (Input.GetKey(KeyCode.F1))
        {
            MB.GiveCommand(ActOp[0]);
        }
        else if (Input.GetKey(KeyCode.F2))
        {
            MB.GiveCommand(ActOp[1]);
        }
        else if (Input.GetKey(KeyCode.F3))
        {
            MB.GiveCommand(ActOp[2]);
        }
        else if (Input.GetKey(KeyCode.F4))
        {
            MB.GiveCommand(ActOp[3]);
        }
        else if (Input.GetKey(KeyCode.F5))
        {
            MB.GiveCommand(ActOp[4]);
        }
        else
        {
            //Idle
            MB.GiveCommand(ActOp[5]);
        }
    }
}
