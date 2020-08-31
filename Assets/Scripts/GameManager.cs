using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI headingText;

    [SerializeField]
    private Text seriesQuestion = null;

    private DataController dataController;
    private Question currentQuestion;
    private int currentQuestionIndex;

    private readonly System.Random _random = new System.Random();

    void Start()
    {
        // Load game data
        dataController = FindObjectOfType<DataController>();
        List<Question> unansweredQuestionsList = dataController.getUnansweredQuestionsList();

        if (unansweredQuestionsList != null && unansweredQuestionsList.Count > 0) {
            int randomIndex = getRandomNumber(unansweredQuestionsList.Count);
            // update current question index
            this.currentQuestionIndex = randomIndex;

            currentQuestion = this.getCurrentQuestion(unansweredQuestionsList, currentQuestionIndex);

            headingText.text = currentQuestion.label;
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

    private int getRandomNumber(int max) {
        return _random.Next(0, max);
    }
}
