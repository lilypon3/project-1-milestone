using System.Collections;
using NUnit.Framework;
using UnityEngine;

public static class CoroutineUtilities
{
    public static IEnumerator moveObjectOverTime(Transform objTransform, 
                                                    Vector3 initialPos,
                                                    Vector3 finalPos,
                                                    float durationSec,
                                                    CoroutineParameters coroutineParam)
    {
        coroutineParam.setRunning(true);

        float initialTime = Time.time;
        float progress = (Time.time - initialTime) / durationSec;

        while (progress < 1f)
        {
            progress = (Time.time - initialTime) / durationSec;
            // use Vector3.Lerp 
            objTransform.position = Vector3.Lerp(initialPos, finalPos, progress);
            yield return null;
        }

        objTransform.position = finalPos;

        coroutineParam.setRunning(false);
    }
}

public class CoroutineParameters
{
    bool running;
    public CoroutineParameters(bool _isRunning) { running = _isRunning; }
    public void setRunning(bool _isRunning) { running = _isRunning; }
    public bool getRunning() { return running; }

}