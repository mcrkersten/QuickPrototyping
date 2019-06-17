using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the type of grab this object is.
/// </summary>
public enum ObjectGrabTypes {
    /// <summary>
    /// Grab on single button-press
    /// </summary>
    GRABLOCK = 0,
    /// <summary>
    /// Grab and hold button
    /// </summary>
    GRABHOLD,
    /// <summary>
    /// Can not hold item, only interact on button-press
    /// </summary>
    INTERACT,
}

public enum WeaponType {
    NOWEAPON = 0,
    Handgun,
    Sniper,
}
