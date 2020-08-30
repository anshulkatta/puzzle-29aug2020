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

    public List<Question> unansweredQuestions;
    public Question[] questionsData = null;
    public Question currentQuestion;
    /* Use this to remove question already answered when GameManager is fixed */
    public int currentQuestionIndex;
    private string gameDataFileName = "newdata.json";
    private readonly System.Random _random = new System.Random();

    void Start()
    {
        // Load game data
        questionsData = getGameData();
        unansweredQuestions = questionsData.ToList<Question>();

        int randomIndex = getRandomNumber(unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomIndex];
        currentQuestionIndex = randomIndex;

        headingText.text = currentQuestion.label;
        seriesQuestion.text = string.Join(",", currentQuestion.questionSeriesData);
    }

	public int getRandomNumber(int max)  
	{  
	  return _random.Next(0, max);
	}

    /*  
     * returns Question[]  
     * by parsing JSON
     */
    private Question[] getGameData()
    {
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        string jsonData = null;
        Question[] loadedData = null;
        //use this in case of apk file
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(filePath);
            www.SendWebRequest();
            while (!www.isDone)
            {
                jsonData = www.downloadHandler.text;
                // Pass the json to JsonUtility, and tell it to create a GameData object from it
                loadedData = JsonHelper.FromJson<Question>(jsonData);
                // Retrieve the allRoundData property of loadedData
                return loadedData;
            }
        }
        // dev mode
        else if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            loadedData = JsonHelper.FromJson<Question>(jsonData);
            return loadedData;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }

        return null;
    }

}
