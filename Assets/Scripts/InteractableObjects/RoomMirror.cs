using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMirror : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    public void Interact(DialogManager dialogManager)
    {
        StartCoroutine(dialogManager.ShowDialog(dialog));
    }
}
