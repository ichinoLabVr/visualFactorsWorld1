using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Rendering.PostProcessing;


public class DepthField : MonoBehaviourPunCallbacks
{
    private Rigidbody rb;
    private Vector3 cleanField;
    private Vector3 me;
    private float dis;
    private PostProcessVolume m_Volume;
    private DepthOfField m_DepthOfField;
    private GameObject postProcessGameObject;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        cleanField = GameObject.Find("DepthFieldArea").transform.position;
        postProcessGameObject = GameObject.Find("PostProcessingGO");
        m_DepthOfField = ScriptableObject.CreateInstance<DepthOfField>();
        m_DepthOfField.enabled.Override(true);
        m_DepthOfField.focalLength.Override(dis * 2);
        m_Volume = PostProcessManager.instance.QuickVolume(postProcessGameObject.layer, 0f, m_DepthOfField);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (!rb.IsSleeping())
            {
                me = this.gameObject.transform.position;
                dis = Vector3.Distance(cleanField, me);
                m_DepthOfField.focalLength.value = dis * 2;
            }
        }
    }
}