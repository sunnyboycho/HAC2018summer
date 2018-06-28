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
    GameObject[] portraitPoints;

    [SerializeField]
    GameObject[] conversationPoints;

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
        for (int i = 0; i < 4; i++)
        {
            //Debug.Log(j);
            string character = portraitPoints[(i + 1) % 2].GetComponentInChildren<PortraitDisplay>().portrait.characterName;
            conversationPoints[i % 2].transform.GetChild(0).gameObject.SetActive(true);
            conversationPoints[i % 2].GetComponentInChildren<Text>().text = "Hey " + character + "!";
            ReadString();
            yield return new WaitForSeconds(1f);
        }
    }

    void EndConversation()
    {
        conversationPoints[0].transform.GetChild(0).gameObject.SetActive(false);
        conversationPoints[1].transform.GetChild(0).gameObject.SetActive(false);
    }

    static void ReadString()
    {
        string path = "Assets/Resources/test.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
}
