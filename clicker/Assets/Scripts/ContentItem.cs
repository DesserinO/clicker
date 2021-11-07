using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute]
public class ContentItem : ScriptableObject
{
    [SerializeField] string[] names;

    public string[] Names { get { return names;} }
}
