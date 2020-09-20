using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMineLight : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer Light;

    private void Start()
    {
        LightSwitch();
        Light.enabled = false;
    }

    public void LightSwitch()
    {
        StartCoroutine(OnOff());
    }

    IEnumerator OnOff()
    {
        Light.enabled = !Light.enabled;

        yield return new WaitForSeconds(1f);
        LightSwitch();

    }
}

