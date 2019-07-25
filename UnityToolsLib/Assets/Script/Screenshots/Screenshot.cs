using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Screenshot : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.S)) {
            string currentDate = DateTime.Now.ToString("dd-MMM-HH-mm-ss");
            string screenShotName = "screenshot_" + currentDate + ".jpg";
            Debug.LogError(screenShotName);
            ScreenCapture.CaptureScreenshot(screenShotName, 4);

            var t = System.IO.File.Exists(Application.dataPath +"/"+ screenShotName);
            Debug.LogError(t + " " + Application.persistentDataPath + "/" + screenShotName);
        }
	}
}
