using UnityEngine;
using System.Collections;



public class AnimatedTexture : MonoBehaviour
{
    public bool changeTextures = true;
    public float Delay = 0.5f;
    public Renderer rend;
    public Texture2D[] textures = new Texture2D[1];

    void Start()
    {
        StartCoroutine(change());
        rend = GetComponent<Renderer>();
    }

    void Update() { }

    IEnumerator change()
    {
        
        while (changeTextures)
        {
            int i = 0;
            do
            {
                yield return new WaitForSeconds(Delay);
                rend.material.SetTexture("_BumpMap", textures[i]);
                i++;

            }
            while (i < textures.Length);
            if (i > textures.Length) { i = 0; }
        }
    }
}