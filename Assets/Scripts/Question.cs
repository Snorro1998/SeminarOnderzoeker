using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Questions/Question")]
public class Question : ScriptableObject
{
    public string firstName;
    public string lastName;
    public int age;
    public string gender;
    public string occupation;
    public string status;
    [TextArea]
    public string description;
    public string quote;

    public string reveal1;
    public string reveal2;
    public string reveal3;
}
