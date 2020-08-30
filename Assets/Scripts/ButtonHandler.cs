using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public InputField inputField;
    Button numberButton;
    void Start()
    {
        numberButton = GetComponent<Button>();
        numberButton.onClick.AddListener(delegate{
            Debug.Log("inside the text"+ numberButton.GetComponentInChildren<Text>().text);
            inputField.text += numberButton.GetComponentInChildren<Text>().text;
        });
    }

    // Update is called once per frame
    void Destroy()
    {
        numberButton.onClick.RemoveAllListeners();   
    }
}
