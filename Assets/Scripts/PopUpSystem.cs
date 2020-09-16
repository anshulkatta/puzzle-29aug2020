using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TextMeshProUGUI hintText;
    public void popUp(string text) {
        popUpBox.SetActive(true);
        hintText.text = text;
        animator.SetTrigger("pop");
    }
}
