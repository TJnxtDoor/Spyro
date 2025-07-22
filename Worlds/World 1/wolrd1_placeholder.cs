using UnityEngine.UI;
using UnityEngine;
using WorldUI;

namespace World1 {
    public class World1 : MonoBehaviour
    {
        public GameObject[] worldTrees;
        public float maxTreeScale = 1.5f;
        public float minTreeScale = 0.5f;
        public float scaleResponseSpeed = 2f;
        public float scaleResponseProgression = 0f;
        private float currentProgression = 0f;
        public float playerHealth = 100f;
        public float playerLives = 5f;
    }

}


