using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI currentLevel;
    private DataController dataController;
    private int levelCounter;
    void Start()
    {
        currentLevel = GetComponent<TextMeshProUGUI>();
        dataController = FindObjectOfType<DataController>();
        levelCounter = dataController.getCurrentLevel()+1;
        currentLevel.text = levelCounter.ToString();
        dataController.setCurrentLevel(levelCounter);
    }

}
