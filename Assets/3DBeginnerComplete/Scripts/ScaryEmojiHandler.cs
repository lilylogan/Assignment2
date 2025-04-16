using System.Collections;
using UnityEngine;

public class ScaryEmojiHandler : MonoBehaviour
{
    public Transform player;           
    public Transform enemiesParent;   
    public GameObject scaryEmojiPrefab;  
    public float scaryRadius = 5f;
    public float emojiCooldown = 3f;

    float cooldownTimer = 0f;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            foreach (Transform enemy in enemiesParent)
            {
                float distance = Vector3.Distance(player.position, enemy.position);
                if (distance < scaryRadius)
                {
                    Vector3 toEnemy = (enemy.position - player.position).normalized;
                    float dot = Vector3.Dot(player.forward, toEnemy);

                    if (dot > 0.5f) // facing enemy
                    {
                        SpawnScaryEmoji();
                        cooldownTimer = emojiCooldown;
                        break;
                    }
                }
            }
        }
    }

    void SpawnScaryEmoji()
    {
        Vector3 spawnPos = player.position + Vector3.up * 2.0f;
        GameObject emoji = Instantiate(scaryEmojiPrefab, spawnPos, Quaternion.identity);
        emoji.transform.SetParent(player);
        StartCoroutine(AnimateAndDestroy(emoji));
    }

    IEnumerator AnimateAndDestroy(GameObject emoji)
    {
        float appearDuration = 0.5f;
        float visibleDuration = 1.0f;
        float fadeOutDuration = 1.0f;

        float timer = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        // Appear (scale up)
        while (timer < appearDuration)
        {
            emoji.transform.localScale = Vector3.Lerp(startScale, endScale, timer / appearDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        emoji.transform.localScale = endScale;

        // Wait while fully visible
        yield return new WaitForSeconds(visibleDuration);

        // Fade out and shrink
        Renderer renderer = emoji.GetComponentInChildren<Renderer>();
        Material mat = renderer.material;
        Color originalColor = mat.color;
        timer = 0f;

        while (timer < fadeOutDuration)
        {
            float t = timer / fadeOutDuration;
            mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1f, 0f, t));
            emoji.transform.localScale = Vector3.Lerp(endScale, startScale, t);
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(emoji);
    }

}
