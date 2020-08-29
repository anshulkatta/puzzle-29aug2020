using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeriesLevelSwitcher : MonoBehaviour
{
    // switch scene if button is clicked
    Button nextSceneChangeButton;
    void Start()
    {
        nextSceneChangeButton = GetComponent<Button>();
        nextSceneChangeButton.onClick.AddListener(delegate {
            SceneManager.LoadScene("SeriesScene");
        });
    }

}
