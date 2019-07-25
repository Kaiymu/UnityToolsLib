using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour {

    public static float[] samples;
    private float[] freqBand = new float[8];
    private float[] bandBuffer = new float[8];

    private float[] _bufferDecrease = new float[8];

    public float[] _freqBandHighest = new float[8];
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];

    public static float amplitude, amplitudeBuffer;
    private float _amplitudeHighest;
    public float audioProfile;

    private AudioSource _audioSource;

    private void Start() {
        samples = new float[AudioManager.Instance.hertz];
        _audioSource = GetComponent<AudioSource>();

        _GetSpectrumAudioSource();
        _MakeFrequencyBands();
        _BandBuffer();
        _CreateAudiobands();
        _AudioProfile(audioProfile);
    }

    void Update() {
        _GetSpectrumAudioSource();
        _MakeFrequencyBands();
        _BandBuffer();
        _CreateAudiobands();
        _GetAmplitude();
    }

    private void _AudioProfile(float audioProfile) {
        for (int i = 0; i < 8; i++) {

            _freqBandHighest[i] = audioProfile;
        }
    }

    private void _GetSpectrumAudioSource() {
        _audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    private void _GetAmplitude() {

        float currentAmplitude = 0f;
        float currentAmplitudeBuffer = 0f;
        for (int i = 0; i < audioBand.Length; i++) {
            currentAmplitude += audioBand[i];
            currentAmplitudeBuffer += audioBandBuffer[i];
        }

        if (currentAmplitude > _amplitudeHighest) {
            _amplitudeHighest = currentAmplitude;
        }

        amplitude = currentAmplitude / _amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / _amplitudeHighest;
    }

    private void _BandBuffer() {
        for (int i = 0; i < 8; ++i) {
            if (freqBand[i] > bandBuffer[i]) {
                bandBuffer[i] = freqBand[i];
                _bufferDecrease[i] = 0.005f;
            }

            if (freqBand[i] < bandBuffer[i]) {
                bandBuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.2f;
            }
        }
    }

    private void _CreateAudiobands() {
        for (int i = 0; i < 8; i++) {
            if (freqBand[i] > _freqBandHighest[i]) {
                _freqBandHighest[i] = freqBand[i];
            }

            audioBand[i] = (freqBand[i] / _freqBandHighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / _freqBandHighest[i]);
        }
    }

    private void _MakeFrequencyBands() {
        int count = 0;

        for (int i = 0; i < freqBand.Length; i++) {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7) {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++) {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBand[i] = average * 10;
        }
    }

    public static float GetFreqOrBandBuffer(bool freqOrBand, int band) {
        return freqOrBand ? audioBandBuffer[band] : audioBand[band];
    }

    public static float GetAmplitudeOrBuffer(bool userBufferAmplitude) {
        return userBufferAmplitude ? amplitudeBuffer : amplitude;
    }
}
