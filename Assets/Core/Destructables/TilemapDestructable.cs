using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TilemapDestructable : MonoBehaviour, IHittable
{
    [SerializeField]
    private GameObject TilemapDestructionEffectReference;
    [SerializeField]
    private float TilemapDestructionEffectDuration = 0.4f;
    private Tilemap tilemap;
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    private void DestroyTile(Vector3Int tilePos)
    {
        tilemap.SetTile(tilePos, null);
        Vector3 newPos = tilemap.CellToWorld(tilePos);
        GameObject newEffect = Instantiate(TilemapDestructionEffectReference,newPos,Quaternion.identity);
        if (newEffect.TryGetComponent(out ParticleSystem particleSystem))
        {
            particleSystem.Play(true);
        }
        Destroy(newEffect, TilemapDestructionEffectDuration);
    }
    public void OnHit(Vector2 position)
    {
        Vector3Int mapPos = tilemap.WorldToCell(position);
        if (mapPos != null && tilemap.HasTile(mapPos))
        {
            DestroyTile(mapPos);
        }
    }
}
