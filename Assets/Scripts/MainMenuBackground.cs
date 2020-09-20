using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackground : MonoBehaviour
{
    private void Update()
    {
        float _backgroundSpeed = 0.05f;
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.y += Time.deltaTime * _backgroundSpeed;

        mat.mainTextureOffset = offset;
    }
}
