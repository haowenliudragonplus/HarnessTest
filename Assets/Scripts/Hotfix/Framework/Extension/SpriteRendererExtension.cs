using UnityEngine;

public static class SpriteRendererExtension
{
    public static void CalcMaxAndMinWorldPos(this SpriteRenderer spriteRenderer,
        out float minX, out float minY, out float maxX, out float maxY)
    {
        float worldX = spriteRenderer.sprite.rect.width / spriteRenderer.sprite.pixelsPerUnit;
        float worldY = spriteRenderer.sprite.rect.height / spriteRenderer.sprite.pixelsPerUnit;
        float vx = worldX * 0.5f;
        float vy = worldY * 0.5f;
        minX = -vx;
        minY = -vy;
        maxX = vx;
        maxY = vy;
    }
}