using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmojiReactor : MonoBehaviour
{
    public GameObject shyEmojiPrefab;
    public Transform player;
    public Transform showerTarget; // an empty GameObject at the center of the shower
    private GameObject currentEmoji;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            Vector3 toShower = (showerTarget.position - player.position).normalized;
            Vector3 playerForward = player.forward;
            float dot = Vector3.Dot(playerForward, toShower);

            if (dot > 0.5f)
            {
                Debug.Log("Player is facing the shower. Dot product: " + dot);

                Vector3 spawnPos = player.position + Vector3.up * 2.0f;
                GameObject emoji = Instantiate(shyEmojiPrefab, spawnPos, Quaternion.identity);
                emoji.transform.SetParent(player);

                StartCoroutine(AnimateAndDestroy(emoji));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player && currentEmoji != null)
        {
            Destroy(currentEmoji);
        }
    }

    IEnumerator AnimateAndDestroy(GameObject emoji)
{
    Vector3 originalScale = emoji.transform.localScale;
    float timer = 0f;
    float duration = 2f; // how long the emoji lasts

    while (timer < duration)
    {
        float scale = 1f + Mathf.Sin(timer * 10f) * 0.1f; // pulsing bounce
        emoji.transform.localScale = originalScale * scale;

        timer += Time.deltaTime;
        yield return null;
    }

    Destroy(emoji);
}

}
