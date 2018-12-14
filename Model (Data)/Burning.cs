using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : Status {
    private float minTickDamage = 5.0f;
    private float maxTickDamage = 10.0f;
    private bool Triggered = false;
    private GameObject FieryVisual;
    private float StartingTime = 0;
    private float Duration = 0;
    private float AccumulateDmg = 0.0f;
    private float ShowDmgAt;
    public override void Init(Transform T)
    {
        Duration = 7.0f;
        StartingTime = Time.time;
        FieryVisual = Instantiate(VisualEffects[1]);
        FieryVisual.transform.SetParent(T);
        FieryVisual.transform.localPosition = new Vector3(0, 1.25f, 0);
        ShowDmgAt = Random.Range(7, 20);
    }
    public override float CurrentStatus(Animator A, MoveXY MXY)
    {
        float CurrentDuration = Time.time - StartingTime;
        if (CurrentDuration > Duration)
        {
            Object.Destroy(FieryVisual);
            Triggered = false;
            return 0;
        }
        AccumulateDmg += -Random.Range(minTickDamage * Time.deltaTime, maxTickDamage * Time.deltaTime);
        
        if(AccumulateDmg < -ShowDmgAt)
        {
            A.GetComponent<CharacterHealth>().UpdateHealth(AccumulateDmg);
            AccumulateDmg = 0.0f;
            ShowDmgAt = Random.Range(7, 20);
        }
        return Duration - CurrentDuration;
    }

    public override void EffectIntensify(float AddDuration)
    {
        Duration += (Duration / 4.0f);
    }
}
