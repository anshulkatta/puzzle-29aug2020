using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    void Awake()
    {
       // if (this.gameObject.active)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
