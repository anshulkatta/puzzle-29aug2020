using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI currentLevel;
    private int level = 0;
    void Start()
    {
        currentLevel = GetComponent<TextMeshProUGUI>();
        currentLevel.text = level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
