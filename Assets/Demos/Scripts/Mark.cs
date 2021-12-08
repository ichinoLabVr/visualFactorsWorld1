using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//visualFactorsWorld1
public class Mark : MonoBehaviourPunCallbacks
{
    private bool _isMark = false;
    public GameObject mark;
    public GameObject mark1;
    public GameObject mark2;
    public GameObject PhotonController;
    public RandomMatchMaker script;
    AudioSource audioSource;
    private Animator anim; // キャラにアタッチされるアニメーターへの参照

    // Start is called before the first frame update
    void Start()
    {
        // int myId = photonView.ViewID;
        // Debug.Log(myId);
        PhotonController = GameObject.Find("PhotonController");
        script = PhotonController.GetComponent<RandomMatchMaker>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>(); // Animatorコンポーネントを取得する
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Mark"))
        {	// 1キーを入力したら
            if (photonView.IsMine)
            {
                photonView.RPC("ChangeMark", RpcTarget.All);
                anim.SetBool("Rest", true);
            }
        }

        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetButtonDown("Start"))
            {
                photonView.RPC("Audiostart", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void ChangeMark()
    {
        mark.SetActive(true);
        mark1.SetActive(true);
        mark2.SetActive(true);
        _isMark = true;
        StartCoroutine("Blink");
        FileLog.AppendLog("log/log.txt", System.DateTime.Now.ToString() + " UserID=" + PhotonNetwork.CurrentRoom.PlayerCount + " Reaction\n");
        audioSource.Play();
    }

    IEnumerator Blink()
    {
        if (_isMark)
        {
            yield return new WaitForSeconds(2.0f); //2秒待って
            mark.SetActive(false);
            mark1.SetActive(false);
            mark2.SetActive(false);
        }
    }
}
