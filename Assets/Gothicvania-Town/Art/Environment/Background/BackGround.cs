using UnityEngine;

public class BackGround : MonoBehaviour
{
    private MeshRenderer m_Renderer;
    private Material m_Material;
    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_Material = GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offsetM = m_Material.mainTextureOffset;
        offsetM.x = transform.position.x;
        offsetM.y = transform.position.y;
        m_Material.mainTextureOffset = offsetM;

    }
}
