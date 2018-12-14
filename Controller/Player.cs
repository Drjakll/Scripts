using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Actions[] ActOp;
    public Actions Block;
    protected Dictionary<string, Status> StatusList = new Dictionary<string, Status>();
    protected ActionCaster MB;
    protected MoveXY MXY;

    public void UploadStatus(Status S, string TypeOfStatus)
    {
        Status S2;
        if (StatusList.TryGetValue(TypeOfStatus, out S2)){
            S2.EffectIntensify(10.0f);
            return;
        }
        S.Init(transform);
        StatusList.Add(TypeOfStatus, S);
    }

    public Dictionary<string, Status> GetStatusList()
    {
        return StatusList;
    }

    public void RemoveStatus(string S)
    {
        StatusList.Remove(S);
    }

    protected bool CurrentStatus()
    {
        bool returnVal = true;
        if (StatusList.Count != 0)
        {
            foreach (KeyValuePair<string, Status> S in StatusList)
            {
                if(S.Value.CurrentStatus(GetComponent<Animator>(), MXY) <= 0)
                {
                    StatusList.Remove(S.Key);
                    return returnVal;
                }
                else if(S.Value.CurrentStatus(GetComponent<Animator>(), MXY) >= 100.0f)
                {
                    returnVal = false;
                }
            }
        }
        return returnVal;
    }
}
