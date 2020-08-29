using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TickButtonHandler : MonoBehaviour
{
    [SerializeField]
    public InputField inputField;
    Button tickButton;
   /* //[SerializeField]*/
    GameManager gameManager;
    Answer correctAnswer;
    [SerializeField]
    Text wrongTextMsgDisplay;
    void Start()
    {
        tickButton = GetComponent<Button>();
        wrongTextMsgDisplay.text = "";
        tickButton.onClick.AddListener(delegate {
            //gameManager = GetComponent<GameManager>();
            gameManager = GameObject.FindObjectOfType<GameManager>();
            Debug.Log("inside the tickbutton" + gameManager.questionsData);
            correctAnswer = gameManager.questionsData[0].answerArray[0];
            if (inputField.text == correctAnswer.answerData.ToString())
            {
                SceneManager.LoadScene("CorrectAnswer");
            }
            else
            {
                displayTextOnWrongAns();
            }
        });
    }

    private void displayTextOnWrongAns() {
        //display text as wrong ans.
        wrongTextMsgDisplay.text = "Wrong Answer";
        StartCoroutine("WaitForSec");
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        wrongTextMsgDisplay.text = "";
    }
}
