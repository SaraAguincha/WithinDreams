using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class Milestones : SerializedScriptableObject
{
    public Dictionary<string, bool> boolMilestones = new Dictionary<string, bool>();
}
