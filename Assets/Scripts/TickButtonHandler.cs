using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    private SaveManager saveManager;
    private DataController dataController;
    private Answer correctAnswer;
    private Dictionary<string, List<Question>> sceneQuestionMap;

    void Start()
    {
        tickButton = GetComponent<Button>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        saveManager = GameObject.FindObjectOfType<SaveManager>();
        dataController = FindObjectOfType<DataController>();
        string filename="save.dat";
        tickButton.onClick.AddListener(delegate
        {
            correctAnswer = gameManager.getAnswerforCurrentQuestion();

            if (inputField.text == correctAnswer.answerData.ToString())
            {
                dataController.removeQuestionFromUnansweredQuestionsList(gameManager.getCurrentQuestionIndex());
                sceneQuestionMap = dataController.getScenesToQuestions();
                string currentkey = SceneManager.GetActiveScene().name;
                List<Question> currentQuesList=sceneQuestionMap[currentkey];
                //list with different scene only remove the question else remove complete object
                if (currentQuesList.Count > 1)
                {
                    currentQuesList.RemoveAt(gameManager.getCurrentQuestionIndex());
                    sceneQuestionMap[currentkey] = currentQuesList;
                }
                else {                   
                    sceneQuestionMap.Remove(currentkey);
                }
                saveManager.saveDataToDisk(filename);
                SceneManager.LoadScene("CorrectAnswer");
            }
            else
            {
                displayTextOnWrongAns();
            }
        });
    }


    private void displayTextOnWrongAns()
    {
        wrongTextMsgDisplay.text = "Wrong Answer";
        inputField.text = "";
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
