﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nivel2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Example());
    }

        IEnumerator Example()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(4);
    }
}
