using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class UpdateSky : MonoBehaviour
{
    private ReflectionProbe baker;

    private void Start()
    {
        baker = gameObject.AddComponent<ReflectionProbe>();
        baker.cullingMask = 0;
        baker.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
        baker.mode = ReflectionProbeMode.Realtime;
        baker.timeSlicingMode = ReflectionProbeTimeSlicingMode.NoTimeSlicing;

        RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;
        StartCoroutine(UpdateEnvironment());
    }

    private IEnumerator UpdateEnvironment()
    {
        DynamicGI.UpdateEnvironment();
        baker.RenderProbe();
        yield return new WaitForEndOfFrame();
    }
}