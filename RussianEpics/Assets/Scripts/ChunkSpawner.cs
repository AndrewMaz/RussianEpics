using UnityEngine;
using UnityEngine.Serialization;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private Chunk[] chunks;
    [SerializeField] private ChunkEnd chunkEnd;
    [SerializeField] private Transform spawnObject;

    private void Start()
    {
        SpawnNextChunk();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out ChunkEnd component))
            SpawnNextChunk();
    }

    private void SpawnNextChunk()
    {
        var x = 0;
        var position = transform.position;
        var chunk = chunks[Random.Range(0, chunks.Length)];
        foreach (var segment in chunk.Segments)
        {
            x++;
            var y = 0;
            foreach (var element in segment.Spawnables)
            {
                y++;
                if(element == null)
                    continue;
                var instantiate = Instantiate(element.gameObject, spawnObject, true);
              
                instantiate.transform.position =
                    new Vector3(position.x + x, position.y + y, 0.0f);
            }
        }
        
        var instantiate1 = Instantiate(chunkEnd);
        
        instantiate1.transform.position =
            new Vector3(position.x + x + 1, position.y, 0.0f);
    }
}
