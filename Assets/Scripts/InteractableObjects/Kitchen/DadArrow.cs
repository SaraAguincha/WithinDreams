using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadArrow : MonoBehaviour
{
    [SerializeField] Milestones milestones;

    void Update()
    {
        if (milestones.getBoolMilestone("dadArrowAfterQuest"))
            this.gameObject.SetActive(false);
    }
}
