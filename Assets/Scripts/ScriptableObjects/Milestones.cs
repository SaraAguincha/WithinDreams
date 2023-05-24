using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class Milestones : SerializedScriptableObject
{
    public Dictionary<string, bool> boolMilestones = new Dictionary<string, bool>();
    //public Dictionary<string, int> intMilestones = new Dictionary<string, int>();

    public bool getBoolMilestone(string milestone)
    {
        if (boolMilestones.ContainsKey(milestone))
        {
            return boolMilestones[milestone];
        }
        return false;
    }

    public void addMilestone(string name, bool value)
    {
        if (boolMilestones.ContainsKey(name))
            boolMilestones[name] = value;
        else
            boolMilestones.Add(name, value);
    }

    /*public void addMilestone(string name, int value)
    {
        intMilestones.Add(name, value);
    }*/

}
