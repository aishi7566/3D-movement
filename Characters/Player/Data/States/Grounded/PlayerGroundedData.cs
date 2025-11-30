using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerGroundedData
{
    [field: SerializeField][field:Range(0f,25f)]public float BassSpeed{get;private set;}
    [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
    [field: SerializeField] public PlayerWalkData WalkData { get; private set; }
    [field: SerializeField] public PlayerRunData RunData { get; private set; }
}
