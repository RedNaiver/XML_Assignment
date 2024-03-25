using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In this little script, we make a constructer for the members of the group.

[Serializable]
public struct Members
{
    public string name;
    public int year;
    public string color;

    public Members(string name, int year, string color)
    {
        this.name = name;
        this.year = year;
        this.color = color;
    }

    [Serializable]
    public class MemberList
    { public List<Members> list; }
}
