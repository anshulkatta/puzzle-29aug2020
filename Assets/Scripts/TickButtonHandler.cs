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
    private Text wrongTextMsgDisplay;

    private Button tickButton;
    private GameManager gameManager;
    private DataController dataController;
    private Answer correctAnswer;

    private GameObject prefabGameObj;
    int id;

    void Start()
    {
        tickButton = GetComponent<Button>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        dataController = FindObjectOfType<DataController>();

        tickButton.onClick.AddListener(delegate {
            correctAnswer = gameManager.getAnswerforCurrentQuestion();

            if (inputField.text == correctAnswer.answerData.ToString())
            {
                dataController.removeQuestionFromUnansweredQuestionsList(gameManager.getCurrentQuestionIndex());
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
