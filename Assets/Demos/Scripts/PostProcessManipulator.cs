using UnityEngine;
using UnityEngine.Rendering.PostProcessing; //追加を忘れずに！

public class PostProcessManipulator : MonoBehaviour {

    PostProcessVolume m_Volume;
    DepthOfField m_DepthOfField;

    GameObject postProcessGameObject;

    void Start ()
    {
        postProcessGameObject = GameObject.Find("PostProcessingGO");
        m_DepthOfField = ScriptableObject.CreateInstance<DepthOfField>();
        m_DepthOfField.enabled.Override(true);
        m_DepthOfField.focalLength.Override(12f);

        m_Volume = PostProcessManager.instance.QuickVolume(postProcessGameObject.layer, 100f, m_DepthOfField);
    }
	
    void Update ()
    {
      
    }
}
