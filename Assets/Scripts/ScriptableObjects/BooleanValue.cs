using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BooleanValue : ScriptableObject
{
    public bool value;

    public bool getValue() { return this.value; }
    public void setFalse() { this.value = false; }
    public void setTrue() { this.value = true;}
}
