using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Apple", menuName = "Apple Chance", order = 51)]
public class AppleChance : ScriptableObject
{
    [SerializeField] private GameObject applePrefabs;
    [Range(0, 1)][SerializeField] private float chance;
    [SerializeField] private float angle;

    public GameObject Apple { get { return applePrefabs; } }
    public float Chance { get { return chance; } }
    public float Angle { get { return angle; } }
}
