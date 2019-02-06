using UnityEngine;
using System.IO;

public class ItemsJsonSerilization : MonoBehaviour
{

    public Items[] items;
    string JSONpath;
    string jsonString;
   
    // Use this for initialization
    void Start ()
    {
        // !!! this class is for development purpose only !!!
        InitJSONSerialization();
    }
    // start serializing the items class to JSON String...
    public void InitJSONSerialization()
    {
        JSONpath = Application.dataPath + "/ItemCategory.json";
        jsonString = JsonUtility.ToJson(items);
        File.WriteAllText(JSONpath, jsonString);
        Debug.Log("T R U E");
    }

}