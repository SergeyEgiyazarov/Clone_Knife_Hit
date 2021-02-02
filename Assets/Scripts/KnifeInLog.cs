using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Knife", menuName = "Knife in Log", order = 51)]
public class KnifeInLog : ScriptableObject
{
    [SerializeField] private GameObject knifePrefabs;
    [SerializeField] private float knifeAngle;

    public GameObject Knife { get { return knifePrefabs; } }
    public float Angle { get { return knifeAngle; } }
}
