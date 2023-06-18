using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialArrow : MonoBehaviour
{
    [SerializeField] Milestones milestones;

    private void Awake()
    {
        if (milestones.getBoolMilestone("dreamWorldUnlocked"))
            this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (milestones.getBoolMilestone("dreamWorldUnlocked"))
            this.gameObject.SetActive(false);
    }
}
