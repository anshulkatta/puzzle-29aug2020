using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro;

public class FeedBackHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI feedBackQuestion1;

    [SerializeField]
    private TextMeshProUGUI feedBackQuestion2;

    [SerializeField]
    private TMP_InputField inputField1;

    [SerializeField]
    private TMP_InputField inputField2;

    [SerializeField]
    private Button submitFeedBackButton;

    private SaveManager saveManager;
    private string filename = "save.dat";

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        submitFeedBackButton.onClick.AddListener(delegate {
            List<string> puzzleFeedBack = new List<string>();
            FeedBack feedBack = new FeedBack();
            if (inputField1.text != null) {
                feedBack.question1 = feedBackQuestion1.text;
                feedBack.answer1 = inputField1.text;
            }

            if (inputField2.text != null) {
                feedBack.question2 = feedBackQuestion2.text;
                feedBack.answer2 = inputField2.text;
            }

            submitFeedBack(feedBack);

            bool isFileDeleted = saveManager.deleteData(filename);
            SceneManager.LoadScene("GameOver");

        });
    }

    private void submitFeedBack(FeedBack feedBack) {
        Debug.Log(feedBack.question1);
    }

    class FeedBack {
        public string question1;
        public string question2;

        public string answer1;
        public string answer2;
    }

}
