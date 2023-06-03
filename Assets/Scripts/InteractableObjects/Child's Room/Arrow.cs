using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField] Milestones milestones;

    // Start is called before the first frame update
    void Start()
    {
        if (milestones.getBoolMilestone("dreamWorldUnlocked"))
            this.gameObject.SetActive(false);
    }
}
