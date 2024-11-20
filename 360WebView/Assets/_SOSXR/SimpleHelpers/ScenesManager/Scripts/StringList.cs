using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "mrstruijk/Variables/StringList", fileName = "StringList")]
public class StringList : ScriptableObject
{
    [SerializeField] private List<string> strings;

    public List<string> Strings => strings;


    public void Add(string str)
    {
        strings.Add(str);
    }
}