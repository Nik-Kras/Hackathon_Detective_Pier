using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Image portretImage;
	public Text nameText;
	public Text dialogueText;
	public VerticalLayoutGroup questionLayoutGroup;
	public GameObject prefabQuestionChoose;

	public Animator animator;

	private Dialogue currentDialogue;
	private Queue<string> sentences;

	public static DialogueManager instance = null; // Экземпляр объекта

	// Use this for initialization
	void Start () 
	{
		sentences = new Queue<string>();
		instance = this;
	}

	public void StartDialogue (string name, Sprite portret, Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = name;
		portretImage.sprite = portret;

		ContinueDialogue(dialogue);
	}

	private void ContinueDialogue(Dialogue dialogue)
	{
		sentences.Clear();
		currentDialogue = dialogue;
		
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		foreach (Transform child in questionLayoutGroup.transform) 
		{
			Destroy(child.gameObject);
		}

		questionLayoutGroup.gameObject.SetActive(false);

		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.025f);
		}
		
		// end typing
		questionLayoutGroup.gameObject.SetActive(true);
		if (sentences.Count == 0 && currentDialogue.questions)
		{
			SentencesFinished();
		}
		else
		{
			AddQuestion("[Continue]").GetComponentInChildren<Button>().onClick.AddListener(DisplayNextSentence);
		}
	}

	void SentencesFinished()
	{
		if (currentDialogue.questions)
		{
			FillQuestions();
		}
	}

	void FillQuestions()
	{
		foreach (Question question in currentDialogue.questions.questions)
		{
			if (question.inventoryContainsObjectKey != "")
			{
				if (!Inventory.instance.objectKeys.Contains(question.inventoryContainsObjectKey))
				{
					continue;
				}
			}
			
			AddQuestion(question.question).GetComponentInChildren<Button>().onClick.AddListener(() => { SelectQuestion(question); });
		}
	}
	
	void SelectQuestion(Question question)
	{
		if (question.endConversation)
		{
			EndDialogue();
		}
		else
		{
			ContinueDialogue(question.dialogue);
		}
	}

	GameObject AddQuestion(string questionText)
	{
		GameObject Instance = Instantiate(prefabQuestionChoose, Vector3.zero, Quaternion.identity);
		Instance.GetComponent<QuestionChoose>().text.text = questionText;
		Instance.transform.SetParent(questionLayoutGroup.transform);
		Instance.transform.SetAsFirstSibling();

		return Instance;
	}
	
	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}
}