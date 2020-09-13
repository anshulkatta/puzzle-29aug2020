using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System;

public class DataController : MonoBehaviour
{
    public SeriesLevel[] seriesLevel;
    private string gameDataFileName = "newdata.json";
    private Question[] questionsData = null;
    private List<Question> unansweredQuestions = null;
    private string[] scenes;
    private Dictionary<string, List<Question>> sceneQuestionMap = new Dictionary<string, List<Question>>();
    private readonly System.Random _random = new System.Random();
    private int level=0;
    private SaveManager saveManager;

    void Start()
    {
        string fileName = "save.dat";
        saveManager = GameObject.FindObjectOfType<SaveManager>();
        DontDestroyOnLoad(gameObject);
            if(saveManager.loadDataFromDisk(fileName))
            {
                unansweredQuestions = sceneQuestionMap.SelectMany(d => d.Value).ToList();
                int randomNum = this.getRandomNumber(sceneQuestionMap.Count - 1);
                SceneManager.LoadScene(sceneQuestionMap.ElementAt(randomNum).Key);
            }     
        else
        {
            /*bool isFileDel = saveManager.deleteData();
            if (isFileDel) {
                Debug.Log("File Deleted");
            }*/
            questionsData = populateGameData();
            unansweredQuestions = questionsData.ToList<Question>();
            setAllSceneInList();
            SceneManager.LoadScene(getCurrentScene(scenes));
        }
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
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        string jsonData = null;
        
        if (Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("Android Platform");
            return fetchDatainAndroid(filePath);
        }
        // dev mode
        else if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
            return parseJsonData(jsonData);
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }

        return null;
    }

    private Question[] fetchDatainAndroid(string filePath) {
        Question[] loadedData = null;
        string jsonData = null;

        WWW www = new WWW(filePath);
        Debug.Log("Sending request");

        while (!www.isDone) { }

        jsonData = www.text;
        return parseJsonData(jsonData);
    }

    public Question[] parseJsonData(string jsonData) {
        return JsonHelper.FromJson<Question>(jsonData);
    }

    public SeriesLevel getCurrentSeriesLevels() {
        return seriesLevel[0];
    }

    private void setAllSceneInList() {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        scenes = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
        }
        setSceneWithQuestion();
    }

    private void setSceneWithQuestion() {
        foreach (string sceneName in scenes) {
            if (sceneName == "SeriesScene")
            {
                List<Question> seriesScenelist = unansweredQuestions.Where(o => o.puzzleType == "seriesNumberPuzzle").ToList();
                sceneQuestionMap.Add(sceneName, seriesScenelist);
            }
            else if (sceneName == "CountImageByFindingHiddenImage")
            {
                List<Question> countImageHidden = unansweredQuestions.Where(o => o.puzzleType == "countImageByFindingHiddenImage").ToList();
                sceneQuestionMap.Add(sceneName, countImageHidden);
            }
            else if (sceneName == "CountImageTotal")
            {
                List<Question> countImage = unansweredQuestions.Where(o => o.puzzleType == "countImageTotal").ToList();
                sceneQuestionMap.Add(sceneName, countImage);
            }
            else if (sceneName == "CountTriangle")
            {
                List<Question> countTriangle = unansweredQuestions.Where(o => o.puzzleType == "countTriangle").ToList();
                sceneQuestionMap.Add(sceneName, countTriangle);
            }
            else if (sceneName == "TextSimplePuzzle")
            {
                List<Question> textSimplePuzzle = unansweredQuestions.Where(o => o.puzzleType == "textSimplePuzzle").ToList();
                sceneQuestionMap.Add(sceneName, textSimplePuzzle);
            }
            else if (sceneName == "TShirtProblem")
            {
                List<Question> tShirtProblem = unansweredQuestions.Where(o => o.puzzleType == "tShirtProblem").ToList();
                sceneQuestionMap.Add(sceneName, tShirtProblem);
            }
        }
    }

    public void setScenesToQuestions(Dictionary<string, List<Question>> dictionaryVar)
    {
        this.sceneQuestionMap= dictionaryVar;
    }
    public Dictionary<string, List<Question>> getScenesToQuestions() {
        return this.sceneQuestionMap;
    }

    public int getRandomNumber(int max)
    {
        return _random.Next(0, max);
    }

    public String getCurrentScene(string[] sceneArray) {
        int randomNum = this.getRandomNumber(sceneArray.Length-2);
        return sceneArray[randomNum];
    }

    public int getCurrentLevel() {
        return level;
    }

    public void setCurrentLevel(int level)
    {
         this.level = level;
    }
}
