using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent<IAddCrystal>(out var comp)) {
            comp.AddCrystal();
            GameObject.Destroy(gameObject);
        }
    }
}
