using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeValue : MonoBehaviour {

    public float startScale, maxScale;
    public bool userBufferAmplitude;
    [Header("Color for the element")]
    public float red, green, blue;

    private Material _material;

    private void Start() {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void Update() {
        _GetAmplitudeValue();
    }

    private void _GetAmplitudeValue() {
        float amplitudeOrAmplitudeBuffer = AudioPeer.GetAmplitudeOrBuffer(userBufferAmplitude);
        Color color = new Color(red * amplitudeOrAmplitudeBuffer, green * amplitudeOrAmplitudeBuffer, blue * amplitudeOrAmplitudeBuffer);
        _material.SetColor("_EmissionColor", color);

        float transformScale = (amplitudeOrAmplitudeBuffer * maxScale) + startScale;
        transform.localScale = new Vector3(transformScale, transformScale, transformScale);
    }
}
