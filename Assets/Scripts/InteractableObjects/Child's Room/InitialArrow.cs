using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialArrow : MonoBehaviour
{
    [SerializeField] Milestones milestones;

    void Update()
    {
        if (milestones.getBoolMilestone("dreamWorldUnlocked"))
            this.gameObject.SetActive(false);
    }
}
