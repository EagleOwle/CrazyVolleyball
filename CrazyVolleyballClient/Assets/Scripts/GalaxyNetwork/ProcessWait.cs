using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessWait : MonoBehaviour
{
    [SerializeField] private float waitTime = 5;
    [SerializeField] private Text messageText;

    private float currentTime;

    private void OnEnable()
    {
        currentTime = 0;
        messageText.text = "Connect";
    }

    private void Update()
    {
        if (waitTime == 0)
            return;

        currentTime += Time.deltaTime;

        messageText.text = "Connect  " + currentTime.ToString("f0");

        if (currentTime >= waitTime)
        {
            gameObject.SetActive(false);
        }
    }

}
