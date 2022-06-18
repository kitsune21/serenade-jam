using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Clip", menuName = "Clip")]
public class ClipScript : ScriptableObject
{
    public string clipName;
    public AudioClip clip;
}
