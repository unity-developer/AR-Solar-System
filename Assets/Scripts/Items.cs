using UnityEngine;

// base class for storing and creating items menu...
//This script is contain the planet elements

[System.Serializable]
public class Items
{
    public string PlanetName;
    public BasicInformation basicInformation;
    public FullInformation fullInformation;
    public MeasurmentDetails measurmentDetails;
}

[System.Serializable]
public class BasicInformation
{
    public string[] context;
}

[System.Serializable]
public class FullInformation
{
    public string text;
}

[System.Serializable]
public class MeasurmentDetails
{
    public string mass;
    public string Volume;
    public string gravity;
    public string oneyear;
}

