using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour {

    [SerializeField]
    TextAsset fileName;

    bool ConversationStarted = false;

    ButtonScript buttonScript;

    [SerializeField]
    GameObject[] portraitPoints = new GameObject[2];

    [SerializeField]
    GameObject[] conversationPoints = new GameObject[2];

    [SerializeField]
    float conversationSpeed;

    int[,] progress =
    {
        { 0, 0, 0, 0, },
        { 0, 0, 0, 0, },
        { 0, 0, 0, 0, }
    };

    // Use this for initialization
    void Start () {
        buttonScript = gameObject.GetComponent<ButtonScript>();

    }
	
	// Update is called once per frame
	void Update () {
        CheckConversationStartable();
        if (Input.GetButtonDown("Jump"))
        {
            StopCoroutine("HoldConversation");
            ConversationStarted = false;
            EndConversation();
        }
    }

    void CheckConversationStartable()
    {
        if (buttonScript.PointAOccupied && buttonScript.PointBOccupied && !ConversationStarted)
        {
            ConversationStarted = true;
            StartConversation();
        }
    }

    void StartConversation()
    {
        StartCoroutine("HoldConversation");
    }

    IEnumerator HoldConversation()
    {
        yield return new WaitForSeconds(.5f);
        string temp = ReadConversationFile("test.txt");
        string[] lines = temp.Split('\n');
        string character = portraitPoints[0].GetComponentInChildren<PortraitDisplay>().portrait.characterName;
        for (int i = 0; i < lines.Length; i+=2)
        {
            int j = 1;
            if (lines[i].Contains(character))
            {
                j = 0;
            }
            conversationPoints[j].transform.GetChild(0).gameObject.SetActive(true);
            conversationPoints[(j+1)%2].transform.GetChild(0).gameObject.SetActive(false);
            conversationPoints[j].GetComponentInChildren<Text>().text = lines[i+1];
            yield return new WaitForSeconds(conversationSpeed);
        }
    }

    void EndConversation()
    {
        conversationPoints[0].transform.GetChild(0).gameObject.SetActive(false);
        conversationPoints[1].transform.GetChild(0).gameObject.SetActive(false);
    }

    string ReadConversationFile(string file)
    {
        string path = "Assets/Resources/" + file;

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string temp = reader.ReadToEnd();
        reader.Close();
        return temp;
    }
}
