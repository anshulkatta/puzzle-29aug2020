using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    public SeriesLevel[] seriesLevel;
    public Question[] questions;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("SeriesScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SeriesLevel getCurrentSeriesLevels() {
        return seriesLevel[0];
    }

    public Question getCurrentQuestionLevels()
    {
        return questions[0];
    }

}
