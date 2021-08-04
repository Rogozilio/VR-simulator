using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void OpenDoor()
    {
        Debug.Log("Finish");
    }

    public IEnumerator Corutina()
    {
        int i = 0;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log(i++);
        }
    }
}
