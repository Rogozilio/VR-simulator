using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISaveLoad<T>
{
    void SaveData(T value);
    T LoadData();
    void ResetData();
}
