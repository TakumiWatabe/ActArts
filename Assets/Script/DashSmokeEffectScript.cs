using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSmokeEffectScript : MonoBehaviour {

    [SerializeField]
    GameObject player;
    [SerializeField]
    PlayerController playerController;
    ParticleSystem particleSystem;
    [SerializeField]
    bool Playstate;
    [SerializeField]
    bool Pausestate;

    // Use this for initialization
    void Start () {
        particleSystem = GetComponent<ParticleSystem>();

        player = transform.root.gameObject;
        playerController = player.GetComponent<PlayerController>();
        transform.parent = null;
        particleSystem.Stop();
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = player.transform.position;

        pos.x -= 0.5f * playerController.Direction;
        pos.y = 0;

        transform.position = pos;

        Playstate = particleSystem.isPlaying;
        Pausestate = particleSystem.isPaused;

        if (playerController.State == "Dash")
        {
            if (!particleSystem.isPlaying) particleSystem.Play();
        }
        else
        {
            if (particleSystem.isPlaying) particleSystem.Stop();

        }
	}
}
