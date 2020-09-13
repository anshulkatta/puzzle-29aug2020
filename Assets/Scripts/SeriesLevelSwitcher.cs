using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeriesLevelSwitcher : MonoBehaviour
{
    // switch scene if button is clicked
    Button nextSceneChangeButton;
    DataController dataController;
    private Dictionary<string, List<Question>> sceneQuestionMap;
    void Start()
    {
        Debug.Log("inside switcher");
        //LeanTween.scale(GameObject.FindWithTag("BaloonImage"), new Vector3(2, 2,2), 4f).setEase(LeanTweenType.easeOutElastic).setDestroyOnComplete(true);
        LeanTween.moveY(GameObject.FindWithTag("BaloonImage"), 1500f, 8f);
        LeanTween.moveY(GameObject.FindWithTag("BaloonImage2"), 1500f, 8f);
        nextSceneChangeButton = GetComponent<Button>();
        dataController = FindObjectOfType<DataController>();
        sceneQuestionMap = dataController.getScenesToQuestions();
        nextSceneChangeButton.onClick.AddListener(delegate {
            if (sceneQuestionMap.Count > 0)
            {
                int randomNum = dataController.getRandomNumber(sceneQuestionMap.Count-1);
                SceneManager.LoadScene(sceneQuestionMap.ElementAt(randomNum).Key);
            }
            else {
                Debug.Log("loading feedback");
                SceneManager.LoadScene("FeedbackScene");
            }
        });
    }

}
