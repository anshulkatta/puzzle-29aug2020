using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class FeedBackHandler : MonoBehaviour
{

    [SerializeField]
    public Toggle optionA;

    [SerializeField]
    public Toggle optionB;

    [SerializeField]
    public Toggle optionC;

    [SerializeField]
    public Toggle optionD;

    [SerializeField]
    private Button submitFeedBackButton;

    private Dictionary<string, List<Question>> sceneQuestionMap;
    private DataController dataController;

    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        sceneQuestionMap = dataController.getAllScenesToQuestions();
        string currentkey = SceneManager.GetActiveScene().name;

        submitFeedBackButton.onClick.AddListener(delegate {
            List<string> puzzleFeedBack = new List<string>();
            if (optionA.isOn)
            {
                puzzleFeedBack.Add(optionA.GetComponentInChildren<Text>().text);
            }

            if (optionB.isOn)
            {
                puzzleFeedBack.Add(optionB.GetComponentInChildren<Text>().text);
            }

            if (optionC.isOn)
            {
                puzzleFeedBack.Add(optionC.GetComponentInChildren<Text>().text);
            }

            if (optionD.isOn)
            {
                puzzleFeedBack.Add(optionD.GetComponentInChildren<Text>().text);
            }

            if (puzzleFeedBack.Count > 0) {                
                sceneQuestionMap.Remove(currentkey);
                if (sceneQuestionMap.Count > 0)
                {
                    int randomNum = dataController.getRandomNumber(sceneQuestionMap.Count - 1);
                    SceneManager.LoadScene(sceneQuestionMap.ElementAt(randomNum).Key);
                }
            }
        });
    }

}
