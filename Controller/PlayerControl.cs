using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Functions;

public class PlayerControl : Player {

    private byte AttackStatus = 0;
    private Actions[] CurrentActions;
    private Button[] buttons;
    private bool CastingAction;
    public  Canvas UserInterface;
    // Use this for initialization
    void Start () {
        MB = GetComponent<ActionCaster>();
        MXY = GetComponent<MoveXY>();
        buttons = UserInterface.GetComponentsInChildren<Button>();
        CurrentActions = new Actions[4];
        EquipNormalAttack();
        MB.GiveCommand(ActOp[6]);
        CastingAction = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (!CurrentStatus())
            return;

        if (MB.IsFinishCasting() && CastingAction)
        {
            AttackStatus = (byte)((AttackStatus + 1) % 2);
            MB.GiveCommand(ActOp[6]);
            CastingAction = false;
        }

        //These are for buttons. It changes color when the assigned key is pressed, and change it back when it's  released.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            buttons[0].onClick.Invoke();
            buttons[0].GetComponent<Image>().color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            buttons[0].GetComponent<Image>().color = Color.white;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            buttons[1].onClick.Invoke();
            buttons[1].GetComponent<Image>().color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            buttons[1].GetComponent<Image>().color = Color.white;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            buttons[2].onClick.Invoke();
            buttons[2].GetComponent<Image>().color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            buttons[2].GetComponent<Image>().color = Color.white;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            buttons[3].onClick.Invoke();
            buttons[3].GetComponent<Image>().color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            buttons[3].GetComponent<Image>().color = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            buttons[4].onClick.Invoke();
            buttons[4].GetComponent<Image>().color = Color.red;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            buttons[4].GetComponent<Image>().color = Color.white;
        }


        if (Input.GetKey(KeyCode.Space))
        {
            //Guard
            MB.GiveCommand(Block);
            MXY.Move(0, 0, 0);
            return;
        } 
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            MB.GiveCommand(ActOp[6]);
        }
        //Attacks
        else if (Input.GetMouseButtonDown(0))
        {
            switch (AttackStatus)
            {
                case 0:
                    MB.GiveCommand(CurrentActions[0]);
                    break;
                case 1:
                    MB.GiveCommand(CurrentActions[1]);
                    break;
            }
            if (CastingAction)
            {
                CastingAction = false;
                MB.GiveCommand(ActOp[6]);
            }
            else
                CastingAction = true;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            switch (AttackStatus)
            {
                case 0:
                    MB.GiveCommand(CurrentActions[2]);
                    break;
                case 1:
                    MB.GiveCommand(CurrentActions[3]);
                    break;
            }
            if (CastingAction)
            {
                CastingAction = false;
                MB.GiveCommand(ActOp[6]);
            }
            else
                CastingAction = true;
        }

        //Adjusting Q and E for sideway movements
        float SideWay = 0;

        if (Input.GetKey(KeyCode.Q))
        {
            SideWay = -1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            SideWay = 1;
        }

        //Keyboard input for movements
        float Vertical = Input.GetAxis("Vertical");
        float Horizontal = Input.GetAxis("Horizontal");
        
        if(Vertical < 0)
        {
            Vertical /= 4.0f;
        }

        MXY.Move(Horizontal, Vertical, SideWay);
	}

    public void EquipNormalAttack()
    {
        CurrentActions[0] = ActOp[0];
        CurrentActions[1] = ActOp[1];
        CurrentActions[2] = ActOp[2];
        CurrentActions[3] = ActOp[3];
    }

    public void EquipSpellCast(GameObject ElectricalBall)
    {
        for(int i = 0; i < CurrentActions.Length; i++)
        {
            CurrentActions[i] = ActOp[5];
            CurrentActions[i].CurrentSpell = ElectricalBall;
        }
        if (MB.IsWeaponEnchanted())
        {
            MB.EnchantWeapon(null, "");
        }
    }

    public void EnchantFire(Status Effect)
    {
        MB.EnchantWeapon(Effect, "Burn");
        EquipNormalAttack();
    }

    public void EnchantFrost(Status Effect)
    {
        MB.EnchantWeapon(Effect, "Frosted");
        EquipNormalAttack();
    }

    public void Charge()
    {
        for (int i = 0; i < CurrentActions.Length; i++)
        {
            CurrentActions[i] = ActOp[4];
        }
        if (MB.IsWeaponEnchanted())
        {
            MB.EnchantWeapon(null, "");
        }
        MB.GiveCommand(ActOp[4]);
        CastingAction = true;
    }
}
