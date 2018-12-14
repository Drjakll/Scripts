using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actions : MonoBehaviour
{
    protected bool aim = false;
    protected bool DestinationReached = false;
    protected float RotateSpeed;
    public GameObject CurrentSpell;
    public abstract bool CastAction(Animator A, GameObject Weapon, Player Target);
    public abstract bool FinishCasting(Animator A, GameObject Weapon, Player Target);
    public virtual void Init() { aim = false; DestinationReached = false; RotateSpeed = 200.0f; }
    public virtual bool Blocking(Animator A, GameObject Weapon) { return false; }


    protected bool Align(Transform Avatar, Transform Target)
    {
        Vector2 TargetDirection = (new Vector2(Target.position.x - Avatar.position.x, Target.position.z - Avatar.position.z)).normalized;

        Vector2 AvatarDirection = (new Vector2(Avatar.transform.forward.x, Avatar.transform.forward.z)).normalized;

        float MagnitudeDifference = (TargetDirection - AvatarDirection).magnitude;

        if (MagnitudeDifference <= .0075)
        {
            Avatar.GetComponent<Animator>().SetFloat("Turn", 0.0f);
            return true;
        }

        float Angle = Mathf.Acos(AvatarDirection.x);
        Angle = ConvertAngle(Angle, AvatarDirection.y);

        Vector2 Plus = new Vector2(Mathf.Cos(Angle + .017f), Mathf.Sin(Angle + .017f));
        Vector2 Minus = new Vector2(Mathf.Cos(Angle - .017f), Mathf.Sin(Angle - .017f));

        if ((Plus - TargetDirection).magnitude < (Minus - TargetDirection).magnitude)
        {
            Avatar.GetComponent<Animator>().SetFloat("Turn", -1.0f);
            Avatar.GetComponent<Animator>().Play("Turn_Left", 0);
            Avatar.Rotate(new Vector3(0, -RotateSpeed * Time.deltaTime * MagnitudeDifference, 0), Space.Self);
        }
        else
        {
            Avatar.GetComponent<Animator>().SetFloat("Turn", 1.0f);
            Avatar.GetComponent<Animator>().Play("Turn_Right", 0);
            Avatar.Rotate(new Vector3(0, RotateSpeed * Time.deltaTime * MagnitudeDifference, 0), Space.Self);
        }

        return false;
    }

    private float ConvertAngle(float orig, float y)
    {
        return y > 0 ? orig : 2 * Mathf.PI - orig;
    }

    private float DegreeToRadian(float Degree)
    {
        return Degree * (Mathf.PI / 180.0f);
    }

    protected bool RunToTarget(Transform Avatar, Transform Target)
    {
        float Difference = (Avatar.transform.position - Target.transform.position).magnitude;
        if (Difference < 2.0f)
        {
            Avatar.GetComponent<MoveXY>().Move(0, 0, 0);
            return true;
        }
        Avatar.GetComponent<Animator>().Play("Run", 0);
        Avatar.GetComponent<MoveXY>().Move(0, 1, 0);
        return false;
    }
}
