using UnityEngine;
public class PadTrigger : MonoBehaviour
    {
        public GameObject closedlaptop;
        public GameObject openlaptop;
        public GameObject cable;
        public GameObject modem;
        public MeshRenderer screen0;
        public MeshRenderer screen1;
        public MeshRenderer screen2;
        public MeshRenderer screen3;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other == closedlaptop.GetComponent<Collider>())
            {
                Debug.Log("laptop");
                closedlaptop.gameObject.SetActive(false);
                openlaptop.gameObject.SetActive(true);
                screen1.gameObject.SetActive(true);
                screen0.gameObject.SetActive(false);
                screen2.gameObject.SetActive(false);
                screen3.gameObject.SetActive(false);

            }
            if (other == cable.GetComponent<Collider>())
            {
                Debug.Log("cable");
                screen2.gameObject.SetActive(true);
                screen0.gameObject.SetActive(false);
                screen1.gameObject.SetActive(false);
                screen3.gameObject.SetActive(false);
            }
            if (other == modem.GetComponent<Collider>())
            {
                Debug.Log("modem");
                screen3.gameObject.SetActive(true);
                screen0.gameObject.SetActive(false);
                screen2.gameObject.SetActive(false);
                screen1.gameObject.SetActive(false);
            }
        }
    }

