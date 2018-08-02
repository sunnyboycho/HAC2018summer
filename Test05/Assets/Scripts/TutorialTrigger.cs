using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

    public Tutorial tutorial;

    public void triggerTutorial()
    {
        FindObjectOfType<TutorialManager>().StartTutorial(tutorial);
    }
}
