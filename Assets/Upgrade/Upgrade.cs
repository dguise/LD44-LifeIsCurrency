using UnityEngine;
using System.Collections;
using System;

public class Upgrade
{
    public string Title { get; set; }
    public string Description { get; set; }
    public float Cost { get; set; }
    public Action<Player> Purchase { get; set; }
}
