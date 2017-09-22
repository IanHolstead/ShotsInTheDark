using UnityEngine;
using System.Collections.Generic;

public class InGamePanel : MonoBehaviour {

    public int linesOfText = 20;
    public bool showPannel = true;
    Canvas canvas;

    private float counter = 0;

	void Start ()
    {
        canvas = GetComponent<Canvas>();
        SetState(showPannel);
	}
	
	void Update ()
    {
        counter += Time.deltaTime;
	}
    

    public void UpdateLog(LinkedList<LogMessage> messages)
    {
        if (Logger.CurrentLogLevel == LogLevel.None)
        {
            return;
        }

        string logText = "";
        LinkedListNode<LogMessage> currentNode;
        if (messages.Count > 0)
        {
            currentNode = messages.Last;
        }
        else
        {
            return;
        }

        int linesPrinted = 0;
        for (int i = 0; linesPrinted < linesOfText && i < messages.Count; i++)
        {
            if (currentNode.Value.Level > Logger.CurrentLogLevel || currentNode.Value.Level == LogLevel.None)
            {
                //Don't print this entry
                currentNode = currentNode.Previous;
                continue;
            }
            //This puts spaces before the numbers so they all line up
            for (int j = 0; j < 3 - Mathf.Floor(Mathf.Log10(messages.Count - linesPrinted) + 1); j++)
            {
                logText += " ";
            }
            logText += messages.Count - linesPrinted +" " + currentNode.Value.ToString() + "\n";
            currentNode = currentNode.Previous;
            linesPrinted++;
        }

        GetComponentInChildren<UnityEngine.UI.Text>().text = logText;
    }

    public void Enable()
    {
        SetState(true);
    }

    public void Disable()
    {
        SetState(false);
    }

    private void SetState(bool enabled)
    {
        showPannel = enabled;
        canvas.enabled = enabled;
        Logger.useInGameLogger = enabled;
    }
}
