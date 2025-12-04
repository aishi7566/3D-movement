
using UnityEngine;

[CreateAssetMenu(fileName ="Player",menuName ="Custom/Character/Player")]

public class PlayerSO : ScriptableObject
{
   [field: SerializeField] public PlayerGroundedData GroundedData{get; private set;}
   [field: SerializeField] public PlayerAirborneData AirborneData { get; private set; }
}
