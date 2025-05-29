using UnityEngine;

[System.Serializable]
public class Dialogue_Line
{
    public string speakerName;
    [TextArea(2, 5)]
    public string sentence;
}
