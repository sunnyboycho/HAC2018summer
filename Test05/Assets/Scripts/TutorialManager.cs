using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    public Text nameText; //sample
    public Text tutorialText;
    public GameObject box;
    public GameObject box2;

    private Queue<string> Sentences;

	// Use this for initialization
	void Start () {
        Sentences = new Queue<string>();

        Time.timeScale = 0f;
    }

    public void StartTutorial(Tutorial tutorial)
    {
        //Debug.Log("Starting Tutorial With " + tutorial.name);
        nameText.text = tutorial.name;

        Sentences.Clear();

        foreach (string sentence in tutorial.sentences)
        {
            Sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (Sentences.Count == 0)
        {
           // EndTutorial();
            box.SetActive(false);
            box2.SetActive(false);
            Time.timeScale = 1f;
            return;
        }

        string sentence = Sentences.Dequeue();
        tutorialText.text = sentence;
    }

    void EndTutorial()
    {
        //Debug,Log("End of tutorial");
    }
}
