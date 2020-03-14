using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Redline.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
        public Transform broom;
        public Queue<GameObject> brushPatchPool = new Queue<GameObject>();

        [SerializeField] private Transform brushPatch;
        [SerializeField] private Transform brushPatchParent;
        [SerializeField] private RenderTexture rt;
        [SerializeField] private Material Ground;
        [SerializeField] private Texture blackTexture;

        private Texture2D t2d;
        
        private void Awake()
        {
            instance = this;

            Ground.mainTexture = blackTexture;
            t2d = new Texture2D(rt.width, rt.height);
        }

        void Start()
        {
            InitiatePooling();

            // saves texture every 5 seconds
            InvokeRepeating("SaveToMemory", 5f, 5f);
        }

        /// <summary>
        /// Pool color patches to avoid instantiating and destroying
        /// </summary>
        void InitiatePooling()
        {
            for (int i = 0; i < 400; i++)
            {
                GameObject g = Instantiate(brushPatch.gameObject, Vector3.down * 5f, Quaternion.identity, brushPatchParent);
                brushPatchPool.Enqueue(g);
            }
        }

        /// <summary>
        /// Save render texture data in memory and apply to ground texture
        /// </summary>
        private void SaveToMemory()
        {
            StartCoroutine(ieSaveToMemory());
        }

        private IEnumerator ieSaveToMemory()
        {
            yield return new WaitForEndOfFrame();
            RenderTexture.active = rt;
            t2d.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            t2d.Apply();
            Ground.mainTexture = t2d;
        }
    }
}
