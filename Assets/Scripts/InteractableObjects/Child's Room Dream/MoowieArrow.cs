using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoowieArrow : MonoBehaviour
{
    [SerializeField] Milestones milestones;

    void Update()
    {
        if (milestones.getBoolMilestone("firstParkCompleted"))
            this.gameObject.SetActive(false);
        else if (milestones.getBoolMilestone("momParkAfterDad"))
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }
}
