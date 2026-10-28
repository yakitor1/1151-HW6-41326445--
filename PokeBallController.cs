using NUnit.Framework.Constraints;
using UnityEngine;
public class PokeBallController : MonoBehaviour
{
    public float throwForce = 10f; // 往右拋出的力道
    public GameObject catchEffectPrefab; // 放入剛剛做好的特效預製體

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 給精靈球往右與往上的推力，產生拋物線
        rb.AddForce(new Vector3(1f, 0.5f, 0f) * throwForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 確認觸發感測的對象是否為寶可夢
        if (collision.gameObject.CompareTag("pokemon"))
        {
            // 在寶可夢的位置生成收服粒子特效
            if (catchEffectPrefab != null)
            {
                Instantiate(catchEffectPrefab, collision.transform.position, Quaternion.identity);
            }

            // 消除精靈球的動力，讓球直接掉地面
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // 刪除場景上的寶可夢模型
            Destroy(collision.gameObject, 2.0f);
        }
    }
}