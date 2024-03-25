using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using System.Xml.Serialization;

public class DataManager : MonoBehaviour
{
    private string _dataPath;

    private string _xmlMembers;
    private string _jsonMembers;


    //The list of members from the group and their birthyear + favorite color.
    private List<Members> memberList = new List<Members>
    {
        new Members("Astrid",2003,"Orange"),
        new Members("Clara",1998,"Black"),    
        new Members("Benjamin",1998,"Blue"),
        new Members("Christoffer",2002,"Yellow"),
        new Members("Oliver", 2001,"Purple"),
    };
    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data/";
        Debug.Log(_dataPath);


        _xmlMembers = _dataPath + "MembersData.xml";
        _jsonMembers = _dataPath + "MembersData.json";
    }


    // Calling the method... which calls the other methods!
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        NewDirectory();
        SerializeXML();
        DeserializeXML();
        SerializeJSON();
    }

    // Creating a new directory, if one doesn't exist already.
    public void NewDirectory()
    {
        if (Directory.Exists(_dataPath))
        {
            Debug.Log("Directory already exists...");
            return;
        }

        Directory.CreateDirectory(_dataPath);
        Debug.Log("New directory created!");
    }
    
    //page.336
    // 
    public void SerializeXML()
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Members>));

        using (FileStream stream = File.Create(_xmlMembers))
        {
            xmlSerializer.Serialize(stream, memberList);
        }
    }
    //page.336
    public void DeserializeXML()
    {
        if (File.Exists(_xmlMembers))
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Members>));

            using (FileStream stream = File.OpenRead(_xmlMembers))
            {
                var members = (List<Members>)xmlSerializer.Deserialize(stream);

                foreach (var Members in members)
                {
                    Debug.LogFormat(Members.name, Members.year, Members.color);
                }
            }
        }
    }

    // Converting the original data (not the XML), into a JSON. 
    public void SerializeJSON()
    {
        Members.MemberList group = new Members.MemberList();
        group.list = memberList;

        string jsonString = JsonUtility.ToJson(group, true);

        using (StreamWriter stream = File.CreateText(_jsonMembers))
        {
            stream.WriteLine(jsonString);
        }
    }
}
