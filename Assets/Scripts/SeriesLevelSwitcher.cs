using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeriesLevelSwitcher : MonoBehaviour
{
    // switch scene if button is clicked
    Button nextSceneChangeButton;
    private TextMeshProUGUI headingText;
    void Start()
    {
        Debug.Log("inside switcher");
        LeanTween.scale(GameObject.FindWithTag("CorrectSeriesAnsTag"), new Vector3(2, 2,2), 4f).setEase(LeanTweenType.easeOutElastic).setDestroyOnComplete(true);
        LeanTween.moveY(GameObject.FindWithTag("BaloonImage"), 1500f, 8f);
        nextSceneChangeButton = GetComponent<Button>();
        nextSceneChangeButton.onClick.AddListener(delegate {
            SceneManager.LoadScene("SeriesScene");
        });
    }

}
