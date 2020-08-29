using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //public Question[] questions;
    private static List<Question> unansweredQuestions;
    [SerializeField]
    private TextMeshProUGUI headingText;
    [SerializeField]
    private Text seriesQuestion = null;
    public Question[] questionsData;

    private Question currentQuestion;
    private string gameDataFileName = "newdata.json";
    //private DataController dataController;
    void Start()
    {
        //dataController = FindObjectOfType<DataController>();
        LoadGameData();
        //loading questions from gamemangaer runtime data addition
        unansweredQuestions = questionsData.ToList<Question>();
        getRandomQuestion();
        headingText.text = currentQuestion.label;
        seriesQuestion.text = string.Join(",", currentQuestion.questionSeriesData);
    }

    void getRandomQuestion()
    {
        int randomIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomIndex];
    }

    //code for json file to upload
    private void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        string dataAsJson;
        //use this in case of apk file
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(filePath);
            www.SendWebRequest();
            while (!www.isDone)
            {
                dataAsJson = www.downloadHandler.text;
                // Pass the json to JsonUtility, and tell it to create a GameData object from it
                GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
                // Retrieve the allRoundData property of loadedData
                questionsData = loadedData.question;
            }
        }
        //in case of development mode 
        else if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
            // Retrieve the allRoundData property of loadedData
            questionsData = loadedData.question;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

}
