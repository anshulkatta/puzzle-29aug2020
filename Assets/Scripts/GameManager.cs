using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI headingText;

    [SerializeField]
    private Text seriesQuestion = null;

    private DataController dataController;
    private Question currentQuestion;
    private int currentQuestionIndex;


    void Start()
    {
        // Load game data
        dataController = FindObjectOfType<DataController>();
        //List<Question> unansweredQuestionsList = dataController.getUnansweredQuestionsList();
        currentQuestion=this.getQuestionByScene(dataController);
        headingText.text = currentQuestion.label;
        int questionSeriesArg = currentQuestion.questionSeriesData.Length;
        if (questionSeriesArg > 0)
        {
            seriesQuestion.text = string.Join(",", currentQuestion.questionSeriesData);
        }
    }

    private Question getCurrentQuestion(List<Question> unansweredQuestionsList, int currentQuestionIndex) {
        return unansweredQuestionsList[currentQuestionIndex];
    }

    public Answer getAnswerforCurrentQuestion() {
        return this.currentQuestion.answerArray[0];
    }

    public int getCurrentQuestionIndex() {
        return this.currentQuestionIndex;
    }

    public Question getQuestionByScene(DataController dataController)
    {
        Dictionary<string, List<Question>> dictSceneQuestion = dataController.getAllScenesToQuestions();
            string currentSceneName = SceneManager.GetActiveScene().name;
        List<Question> questionList = dictSceneQuestion[currentSceneName];
        int randomIndex = dataController.getRandomNumber(questionList.Count);
        // update current question index
        this.currentQuestionIndex = randomIndex;
        currentQuestion = this.getCurrentQuestion(questionList, currentQuestionIndex);
        return currentQuestion;
    }

}
