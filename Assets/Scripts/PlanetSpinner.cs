using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpinner : MonoBehaviour
{
    [SerializeField] float xRotationSpeed;
    [SerializeField] float yRotationSpeed;
    [SerializeField] float zRotationSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRotationSpeed * Time.deltaTime, yRotationSpeed * Time.deltaTime, zRotationSpeed * Time.deltaTime);
    }
}
