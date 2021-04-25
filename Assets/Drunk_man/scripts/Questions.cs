using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
	[TextArea(3, 10)]
	public string question;
	
	public Dialogue dialogue;

	public string inventoryContainsObjectKey;

	public string sequenceEvent;
	public string evedenceKeyEvent;
	
	public bool endConversation;
}

[CreateAssetMenu(fileName = "Data", menuName = "Questions", order = 1)]
[System.Serializable]
public class Questions : ScriptableObject
{
	public Question[] questions;
}
