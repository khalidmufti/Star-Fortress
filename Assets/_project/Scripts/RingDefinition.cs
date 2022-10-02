using System;
using UnityEngine;

public partial class Shield {
    [SerializeField]

    [Serializable]
    public class RingDefinition {
        public String Name;
        public float Radius;
        public int Segments;
        public Color RingColor;
    }   
    
}
