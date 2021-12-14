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
    public float intensity;
    private float dis;
    PostProcessVolume m_Volume;
    DepthOfField m_DepthOfField;

    GameObject postProcessGameObject;

    void Start()
    {
        cleanField = GameObject.FindGameObjectWithTag("BodyUp").transform.position;
        postProcessGameObject = GameObject.Find("PostProcessingGO");
        m_DepthOfField = ScriptableObject.CreateInstance<DepthOfField>();
        m_DepthOfField.enabled.Override(true);
    }

    // Update is called once per frame
    void Update()
    {
        rb = this.GetComponent<Rigidbody>();
        if (photonView.IsMine)
        {
            if (!rb.IsSleeping())
            {
                me = this.gameObject.transform.position;
                dis = Vector3.Distance(cleanField, me);
                Debug.Log(dis);
                m_DepthOfField.focalLength.Override(dis * 5);
                m_Volume = PostProcessManager.instance.QuickVolume(postProcessGameObject.layer, 100f, m_DepthOfField);
                Debug.Log(m_DepthOfField.focalLength.value);
            }
        }
    }
}
