using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamPrefab : MonoBehaviour {

    public int band;
    public float startScale, scaleMultiplier;
    public bool useBufferFreq;
    private Material _material;

    private void Start() {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void Update () {
        float freqOrBandBuffer = AudioPeer.GetFreqOrBandBuffer(useBufferFreq, band);
        Color color = new Color(freqOrBandBuffer, freqOrBandBuffer, freqOrBandBuffer);
        _material.SetColor("_EmissionColor", color);

        transform.localScale = new Vector3(transform.localScale.x, (freqOrBandBuffer * scaleMultiplier) + startScale, transform.localScale.z);
    }
}
