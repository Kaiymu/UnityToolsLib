using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFrequence : MonoBehaviour {

    public GameObject frequencyPrefab;

    public bool useBuffer;

    private void Start() {
        for(int i = 0; i < AudioManager.Instance.frequency; i++) {
            var o = Instantiate(frequencyPrefab);
            o.transform.SetParent(transform);
            o.transform.localPosition = new Vector3(i * 2 , 0, 10);
            var paramPrefab = o.AddComponent<ParamPrefab>();
            paramPrefab.band = i;
            paramPrefab.startScale = 1;
            paramPrefab.scaleMultiplier = 100;
            paramPrefab.useBufferFreq = useBuffer;
        }
    }

    
}
