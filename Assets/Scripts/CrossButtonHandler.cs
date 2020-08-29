using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossButtonHandler : MonoBehaviour
{
    [SerializeField]
    InputField inputField;
    Button crossButton;
    Text wrongTextMsgDisplay;
    void Start()
    {
        //Get the wrong answer text using below code without mapping in inspector
        wrongTextMsgDisplay = GameObject.FindWithTag("WrongAnsTag").GetComponent<Text>();
        crossButton = GetComponent<Button>();
        crossButton.onClick.AddListener(delegate {
            Debug.Log("inside the text" + crossButton.GetComponentInChildren<Text>().text);
            inputField.text = "";
            wrongTextMsgDisplay.text = "";
        });
    }

    void Destroy()
    {
        crossButton.onClick.RemoveAllListeners();
    }

}
