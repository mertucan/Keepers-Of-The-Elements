using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalEnter : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D capsuleCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Oyuncu karakterinin tag'ını "Player" olarak ayarladığınızdan emin olun
        if (collision.gameObject.tag == "Player")
        {
            // Aktif sahnenin indeksini al
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // Bir sonraki sahne indeksine geçiş yap
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void EnableSpriteRenderer()
    {
        if (spriteRenderer != null && capsuleCollider !=null)
        {
            spriteRenderer.enabled = true;
            capsuleCollider.enabled = true;
        }
    }
}
