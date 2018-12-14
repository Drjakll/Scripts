using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour {
    
    public float InitVel = 2.0f;
    public float Deacceleration = 1f;
    public float MaxDamage;
    public float CurrentDisplayDmg;
    private TextMeshPro TMP;
    // Use this for initialization
    void Start()
    {
        CurrentDisplayDmg = 0;
        TMP = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        InitVel -= Deacceleration * Time.deltaTime;
        if (InitVel >= 0.0f)
        {
            CurrentDisplayDmg -= 100 * Time.deltaTime;
            if (CurrentDisplayDmg <= MaxDamage)
                CurrentDisplayDmg = MaxDamage;
            TMP.text = ((int)CurrentDisplayDmg).ToString();
            transform.Translate(0, InitVel * Time.deltaTime, 0, Space.World);
            transform.LookAt(Camera.main.transform);
            transform.Rotate(new Vector3(0, 180, 0), Space.World);
        }
        else
        {
            Object.Destroy(this.gameObject);
        }
    }
}
