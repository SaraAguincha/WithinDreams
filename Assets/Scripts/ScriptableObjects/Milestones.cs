using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.IO;

[CreateAssetMenu]
public class Milestones : SerializedScriptableObject
{
    public Dictionary<string, bool> boolMilestones = new Dictionary<string, bool>();

    // Metrics

    //public Dictionary<string, string> timeMilestones = new Dictionary<string, string>();

    //public Dictionary<string, int> numberOfInteractions = new Dictionary<string, int>();

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
        {
            boolMilestones.Add(name, value);
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("HH:mm:ss:fff");
            
            string fullPath = Application.persistentDataPath + "/timeMetric.txt";
            using (StreamWriter writer = File.AppendText(fullPath))
            {
                string writeString = formattedDateTime + " " + name;
                writer.WriteLine(writeString);  
            }
        }
    }

    public void incrementInteraction(string interactableObject)
    {
        string fullPath = Application.persistentDataPath + "/interactMetric.txt";
        using (StreamWriter writer = File.AppendText(fullPath))
        {
            string writeString = interactableObject;
            writer.WriteLine(writeString);
        }
    }

}
