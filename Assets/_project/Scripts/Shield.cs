using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Shield : MonoBehaviour {
    [SerializeField] List <RingDefinition> _ringDefinitions = null;
    [SerializeField] GameObject _segmentPrefab = null;
    [SerializeField] float _overlap = 0.1f;
    [SerializeField] float _thickness = 0.05f;
    [SerializeField] float _depth = 0.5f;
    [SerializeField] float _rotationRateBase = 90f;
    [SerializeField] float _extraRotation = 30f;

    List<GameObject>[] _rings;
    List<Transform> _ringTransforms;
    GameObject _shields;
    float _age;
    bool _spinning = false;

    IEnumerator Start() {
        CreateRings();
        while (true) {
            SpinRings();
            yield return null; 
        }
    }

    private void CreateRings() {

        SegmentDefinition segmentDefinition = new SegmentDefinition() {
            Overlap = _overlap,
            Thickness = _thickness,
            Depth = _depth
        };

        _shields = CreateRingSegments (transform.position, _ringDefinitions.ToArray(), segmentDefinition);
        _spinning = true;
    }

    private void SpinRings() {
        if (!_spinning) return;
        _age += Time.deltaTime;

        for (int i=0; i < _shields.transform.childCount; i++) {
            Transform pivot = _shields.transform.GetChild(i);
            float angle = _rotationRateBase + i*_extraRotation;
            angle *= _age;

            //every other ring should rotate in the opposite directon
            angle *= ((i % 2) ==0) ? -1 : 1;

            pivot.localRotation = Quaternion.Euler(0 ,0, angle);

        }

    }

    private GameObject CreateRingSegments(Vector3 position, RingDefinition[] ringDefs, SegmentDefinition segmentDef)
    {
        _ringTransforms = new List<Transform>();
        _rings = new List<GameObject>[ringDefs.Length];

        for (int i=0; i < ringDefs.Length; i++) {
            _rings[i] = new List<GameObject>();
            RingDefinition ringDef = ringDefs[i];

            float circumference = ((float)(ringDef.Radius * 2f * Math.PI));   
            float segmentLength = circumference / ringDef.Segments;
            segmentLength += segmentLength * segmentDef.Overlap;

            // Create ring center and make overall shield object as parent
            Transform ringCenter = new GameObject(ringDef.Name).transform;
            ringCenter.SetParent(transform);
            _ringTransforms.Add(ringCenter);

            // Now generate the segments that form the ring, parent will be the center defined above
            for (int segment = 0; segment < ringDef.Segments; segment++) {
                //Determine angle of each segment and rotate it
                float angle = (360.0f * segment) / ringDef.Segments;
                Quaternion rotation = Quaternion.Euler (0,0,angle);
                GameObject segmentGO = Instantiate (_segmentPrefab);
                segmentGO.transform.localRotation = rotation; 

                //Set ring center as parent of segment; set color of ring; scale the segmemt
                segmentGO.transform.SetParent(ringCenter);
                segmentGO.GetComponent<Renderer>().material.color = ringDef.RingColor;
                segmentGO.transform.localScale = new Vector3(segmentLength, segmentDef.Thickness, segmentDef.Depth);

                //Calculate position of Segment.  Need to get direction of Quaternion so multiple rotation by Vector UP scaled to radius
                Vector3 segmentPos = rotation * Vector3.up * ringDef.Radius;
                segmentGO.transform.position = segmentPos;

                _rings[i].Add(segmentGO);
            }

        }

        transform.position = position;

        return gameObject; 
    }    
}
