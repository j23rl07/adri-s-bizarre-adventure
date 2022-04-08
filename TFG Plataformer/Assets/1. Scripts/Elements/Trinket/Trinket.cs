using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Trinket",menuName="Trinket")]
public class Trinket : ScriptableObject
{
    public string name;
    public string description;
    public Sprite icon;

    public TrinketState TrinketState { get; set; }

    public Trinket()
    {
        TrinketState = new TrinketState();
    }

}
