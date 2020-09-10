﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {       
        submitFeedBackButton.onClick.AddListener(delegate {
            List<string> puzzleFeedBack = new List<string>();
            if (optionA.isOn) {
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
        });
    }

}
