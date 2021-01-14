using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
   
    void Start()
    {
        Screen.SetResolution(1440, 900, false, 60);
    }
}
