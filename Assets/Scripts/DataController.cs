using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class DataController : MonoBehaviour
{
    public SeriesLevel[] seriesLevel;
    private string gameDataFileName = "newdata.json";
    private Question[] questionsData = null;
    private List<Question> unansweredQuestions = null;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("SeriesScene");

        questionsData = populateGameData();

        unansweredQuestions = questionsData.ToList<Question>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Question> getUnansweredQuestionsList() {
        return this.unansweredQuestions;
    }

    public Question[] getQuestionData() {
        return questionsData;
    }

    public void removeQuestionFromUnansweredQuestionsList(int questionIndexToRemove) {
        this.unansweredQuestions.RemoveAt(questionIndexToRemove);
    } 

    /*  
     * returns Question[]  
     * by parsing JSON
     */
    private Question[] populateGameData()
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

    public SeriesLevel getCurrentSeriesLevels() {
        return seriesLevel[0];
    }
    
}
