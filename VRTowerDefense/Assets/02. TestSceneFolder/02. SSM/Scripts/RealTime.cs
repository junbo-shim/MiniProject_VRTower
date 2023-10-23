using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class RealTime : MonoBehaviour
{
    public TMP_Text TimeTxt;
 

    // Start is called before the first frame update
    void Start()
    {
    
        playerDieTime();
    }

    // Update is called once per frame
    void playerDieTime()
    {
        TimeTxt.text = (Time.time - GameManager.instance.startTime).ToString("F"+2);
    }
}
