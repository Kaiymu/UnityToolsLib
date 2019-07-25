using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [Header("Number of heartz, should be a value of 2")]
    public int hertz = 512;

    [Header("Number of frequency, should be a value of 2")]
    public int frequency = 8;

    public static AudioManager Instance;

    private void Awake() {

        if (Instance != null)
            return;

        DontDestroyOnLoad(this);
        Instance = this;
    }
}
