using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    GameObject prefabGameObj;
    int id;
    void Start()
    {
        tickButton = GetComponent<Button>();
        //wrongTextMsgDisplay.text = "";
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
        wrongTextMsgDisplay.text = "Wrong Answer";
        //not working very smooth only runs for first time
        LeanTween.scale(GameObject.FindWithTag("WrongAnsTag"), new Vector3(2, 3, 3), 4f).setEaseInElastic().setOnComplete(onCompleteFunc);
    }

    public void onCompleteFunc() {
        LeanTween.cancel(id);
        wrongTextMsgDisplay.text = "";
    }

}
