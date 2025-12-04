using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BoardGenerator _boardGenerator;

    private void Awake()
    {
        _boardGenerator = GetComponent<BoardGenerator>();
    }

    private void Start()
    {
        _boardGenerator.GenerateTilesBoard();
    }
}