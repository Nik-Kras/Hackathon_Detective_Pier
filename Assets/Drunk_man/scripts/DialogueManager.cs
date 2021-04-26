using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class StringEvent : UnityEvent<string> { }

public class DialogueManager : MonoBehaviour {

	public Image portretImage;
	public Text nameText;
	public Text dialogueText;
	public VerticalLayoutGroup questionLayoutGroup;
	public GameObject prefabQuestionChoose;

	public static DialogueManager instance; // Экземпляр объекта
	[HideInInspector]
	public bool isWin;

	public StringEvent onEvent; 
	public UnityEvent onShowInterface; 
	public UnityEvent onEndGame; 
	public AudioSource _clickPlay;

	[SerializeField] private Animator dialogueAnimator;
	
	private Dialogue currentDialogue;
	private Queue<string> sentences;
	private Animator _currentAnimator;

	private static readonly int IsSpeakingTo = Animator.StringToHash("IsSpeakingTo");
	private static readonly int IsOpen = Animator.StringToHash("IsOpen");

	private List<string> _events;

	// Use this for initialization
	void Start () 
	{
		sentences = new Queue<string>();
		_events = new List<string>();
		instance = this;
	}

	public void StartDialogue (string name, Sprite portret, Dialogue dialogue, Animator animator = null)
	{
		dialogueAnimator.SetBool(IsOpen, true);

		nameText.text = name;
		portretImage.sprite = portret;
		portretImage.enabled = portret;

		ContinueDialogue(dialogue);

		if (_currentAnimator)
			_currentAnimator.SetBool(IsSpeakingTo, false);

		if (animator)
		{
			animator.SetBool(IsSpeakingTo, true);
			_currentAnimator = animator;
		}
	}
	public void EndDialogue()
	{
		dialogueAnimator.SetBool(IsOpen, false);
		
		if (_currentAnimator)
			_currentAnimator.SetBool(IsSpeakingTo, false);
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
		StartCoroutine(TypeSentenceCorontine(sentence));
	}

	IEnumerator TypeSentenceCorontine (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.01f);
		}
		
		// end typing
		questionLayoutGroup.gameObject.SetActive(true);
		if (sentences.Count == 0 && currentDialogue.questions)
		{
			SentencesFinished();
		}
		else
		{
			AddQuestion("[Continue]").GetComponentInChildren<Button>().onClick.AddListener(() => { _clickPlay.Play(); DisplayNextSentence(); });
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
			// we don't create dialog option if player don't have specific item
			if (question.inventoryContainsObjectKey != "")
			{
				if (!Inventory.instance.Has(question.inventoryContainsObjectKey))
					continue;
			}
			
			AddQuestion(question.question).GetComponentInChildren<Button>().onClick.AddListener(() => { _clickPlay.Play(); SelectQuestion(question); });
		}
	}
	
	void SelectQuestion(Question question)
	{
		if (question.endConversation)
		{
			EndDialogue();
		}
		else if (question.dialogue)
		{
			ContinueDialogue(question.dialogue);
		}

		if (question.sequenceEvent != "")
		{
			onEvent.Invoke(question.sequenceEvent);
			_events.Add(question.sequenceEvent);

			if (question.sequenceEvent == "GAME_START")
			{
				onShowInterface.Invoke();
			}
			
			if (question.sequenceEvent == "GAME_END")
			{
				onEndGame.Invoke();
			}
			
			if (question.sequenceEvent == "Verdict_False")
				isWin = false;
			
			if (question.sequenceEvent == "Verdict_True")
				isWin = true;
		}

		if (question.evedenceKeyEvent != "")
		{
			Inventory.instance.OnPickup(question.evedenceKeyEvent);
		}
	}

	GameObject AddQuestion(string questionText)
	{
		GameObject Instance = Instantiate(prefabQuestionChoose, Vector3.zero, Quaternion.identity);
		Instance.GetComponent<QuestionChoose>().text.text = questionText;
		Instance.transform.SetParent(questionLayoutGroup.transform);
		Instance.transform.SetAsFirstSibling();
		
		UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)questionLayoutGroup.transform);

		return Instance;
	}
	
	public List<string> events => _events;
}