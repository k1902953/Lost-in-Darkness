using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
[CreateAssetMenu(fileName="Dialogue file", menuName = "Dialogue File Archive")]
public class Dialogue : ScriptableObject
{
    [TextArea(3,10)]
    public string[] sentences;
}
