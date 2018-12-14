using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frosted : Status {

    private bool Triggered = false;
    private GameObject FrostVisual;
    private float StartingTime = 0;
    private float Duration = 0;

    public override void Init(Transform T)
    {
        Duration = 5.0f;
        StartingTime = Time.time;
        FrostVisual = Instantiate(VisualEffects[1]);
        FrostVisual.transform.SetParent(T);
        FrostVisual.transform.localPosition = new Vector3(0, 1.25f, 0);
    }
    public override float CurrentStatus(Animator A, MoveXY MXY)
    {
        float CurrentDuration = Time.time - StartingTime;
        float retVal = Duration - CurrentDuration;
        MoveXY movement = A.GetComponent<MoveXY>();
        if (CurrentDuration > Duration)
        {
            Object.Destroy(FrostVisual);
            A.speed = A.speed * 2.0f;
            movement.VerticalSpeed = 6.0f;
            Triggered = false;
            return 0;
        }
        if (!Triggered)
        {
            A.speed = A.speed / 2.0f;
            movement.VerticalSpeed /= 2.0f;
        }
        Triggered = true;
        return retVal;
    }

    public override void EffectIntensify(float AddDuration)
    {
        Duration += (Duration / 4.0f);
    }
}
