using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class SpikedHeadScript : MonoBehaviour
{
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private float spawnTime = 2.0f;
    private float spawnTimer;
    
    public SpikedBallScript spikeBallPrefab;
    public Vector2 spikeDir;
    
    private void Update()
    {
        if (spawnTimer >= spawnTime)
        {
            ShootSpike();
            spawnTimer = 0.0f;
        }
        spawnTimer += Time.deltaTime;

    }

    private void ShootSpike()
    {
        SpikedBallScript spike = Instantiate(this.spikeBallPrefab, new Vector3(this.transform.position.x + offsetX, this.transform.position.y + offsetY, this.transform.position.z), this.transform.rotation);
        spike.ProjectSpikedBall(spikeDir);
        if (spikeDir == Vector2.down || spikeDir == Vector2.up)
        {
            Rigidbody2D spikeRB = spike.GetComponent<Rigidbody2D>();
            spikeRB.velocity = new Vector2(0.0f, spikeRB.velocity.y);
        }
        else if (spikeDir == Vector2.left || spikeDir == Vector2.right)
        {
            Rigidbody2D spikeRB = spike.GetComponent<Rigidbody2D>();
            spikeRB.velocity = new Vector2(spikeRB.velocity.x, 0.0f);
        }
    }

}
