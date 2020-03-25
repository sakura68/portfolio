using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enUserBuff
{
    GOLD,
    EXP,
    MAX
}

public class UserBuffIcon : MonoBehaviour
{
    [SerializeField] private enUserBuff _userBuffType;
    public enUserBuff userBuffType { get { return _userBuffType; } }
}
