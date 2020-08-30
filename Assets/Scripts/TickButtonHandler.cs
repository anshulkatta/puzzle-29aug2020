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

    [SerializeField]
    Text wrongTextMsgDisplay;

    Button tickButton;
    GameManager gameManager;
    Answer correctAnswer;
    
    GameObject prefabGameObj;
    int id;

    void Start()
    {
        tickButton = GetComponent<Button>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

        tickButton.onClick.AddListener(delegate {
            //gameManager = GetComponent<GameManager>();
            correctAnswer = gameManager.currentQuestion.answerArray[0];

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
        StartCoroutine("WaitForSec");
        //not working very smooth only runs for first time
        //LeanTween.scale(GameObject.FindWithTag("WrongAnsTag"), new Vector3(2, 3, 3), 4f).setEaseInElastic().setOnComplete(onCompleteFunc);
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        wrongTextMsgDisplay.text = "";
    }

    void Destroy()
    {
        tickButton.onClick.RemoveAllListeners();
    }
}
