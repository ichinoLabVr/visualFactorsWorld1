using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Rendering.PostProcessing;

public class Vignette : MonoBehaviourPunCallbacks
{
    private Rigidbody rb;
    private Vector3 cleanVignette;
    private Vector3 me;
    private float disz;
    private PostProcessVolume m_Volume;
    private Vignette m_Vignette;
    private GameObject postProcessGameObject;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        cleanVignette = GameObject.Find("vignette").transform.position;
        postProcessGameObject = GameObject.Find("vignetteVolume");
        // m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        // m_Vignette.enabled.Override(true);
        // m_Vignette.center.Override((1, 0.5));
        // m_Volume = PostProcessManager.instance.QuickVolume(postProcessGameObject.layer, 100f, m_Vignette);
        // Debug.Log(m_Vignette.center.value);

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (!rb.IsSleeping())
            {
                me = this.gameObject.transform.position;
                disz = cleanVignette.z - me.z;
                Debug.Log(disz);
            }
        }

    }
}
