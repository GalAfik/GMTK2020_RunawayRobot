﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadRobot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GetComponent<Animator>()?.SetTrigger("Die");
    }
}
