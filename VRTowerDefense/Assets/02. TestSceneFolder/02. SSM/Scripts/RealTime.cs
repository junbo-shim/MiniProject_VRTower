using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class RealTime : MonoBehaviour
{
    public TMP_Text TimeTxt;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        playerDieTime();
    }

    // Update is called once per frame
    void playerDieTime()
    {
        TimeTxt.text = (Time.time - startTime).ToString();
    }
}
