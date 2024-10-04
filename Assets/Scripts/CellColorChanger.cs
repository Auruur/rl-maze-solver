using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellColorChanger : MonoBehaviour
{
    private MeshRenderer planeRenderer;
    public Material red, orange, blue;

    private void Start()
    {
        // Ottieni il riferimento al componente Renderer del piano
        planeRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeColor(string color)
    {
        if (color == "red"){
            planeRenderer.material = red;
        }
        else if (color == "orange"){
            planeRenderer.material = orange;
        }
        else if (color == "blue"){
            planeRenderer.material = blue;
        }
    }
}

