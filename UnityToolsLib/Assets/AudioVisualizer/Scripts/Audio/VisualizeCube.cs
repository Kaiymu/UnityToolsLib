using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeCube : MonoBehaviour {

    public GameObject cubePrefab;
    public float maximumScale = 10f;
    public float maxColor = 1f;


    private List<GameObject> _samplePrefabs;

	private void Start () {

        // Dividing the heartz by the number of elements, to get a perfect circle
        float rotationPerBeats = (float)AudioManager.Instance.hertz / 360;
        _samplePrefabs = new List<GameObject>();

        for (int i = 0; i < AudioManager.Instance.hertz; i++) {
            var o = Instantiate(cubePrefab);
            o.transform.position = transform.position;
            o.transform.parent = transform;
            o.name = o.name + " " + i;
            transform.eulerAngles = new Vector3(0, -rotationPerBeats * i, 0f);
            o.transform.position = Vector3.forward * 10;
            _samplePrefabs.Add(o);
        }
	}

    private void Update() {
        if (_samplePrefabs == null)
            return;

        for (int i = 0; i < _samplePrefabs.Count; i++) {
            var sampleCube = _samplePrefabs[i];
            sampleCube.transform.localScale = new Vector3(1, (AudioPeer.samples[i] * maximumScale) + 2, 1);
            float baseColor = (AudioPeer.samples[i] * maxColor);
            float red = Mathf.Cos(baseColor);
            float green = Mathf.Sin(baseColor);
            float blue = Random.Range(0f, 1f) * baseColor;
            sampleCube.GetComponent<Renderer>().material.color = new Color(red, green, blue);
        }
    }
}
