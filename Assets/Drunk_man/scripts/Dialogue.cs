using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Dialogue", order = 1)]
[System.Serializable]
public class Dialogue : ScriptableObject
{
	[TextArea(3, 10)]
	public string[] sentences;
	
	public Questions questions;
}