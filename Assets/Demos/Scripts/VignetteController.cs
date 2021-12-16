using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Rendering.PostProcessing;

public class VignetteController : MonoBehaviourPunCallbacks
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
        postProcessGameObject = GameObject.Find("PostProcessingGO");
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.center.Override(new Vector2(0.5f, 0.5f));
        m_Volume = PostProcessManager.instance.QuickVolume(postProcessGameObject.layer, 100f, m_Vignette);
        Debug.Log(m_Vignette.center.value);

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
                m_Vignette.center.value = new Vector2(0.5f - Mathf.Clamp(disz, -2f, 3f), 0.5f);
            }
        }

    }
}
