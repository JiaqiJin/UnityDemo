using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DialogData : ScriptableObject
{
    public List<DialogContent> contents;
}

[System.Serializable] // se puede visualizar en el editor
public class DialogContent
{
    public string dialogText;
    public bool charaADisplay;
    public bool charaBDisplay;
}
