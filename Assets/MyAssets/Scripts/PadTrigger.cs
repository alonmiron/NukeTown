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
        public AudioSource hint1; //hint 1
        public AudioSource hint2; //hint 2 
        public AudioSource message3;
        
    
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
                //audioSource1 = GetComponent<AudioSource>();
                hint1.PlayDelayed( 4 );

            }
            if (other == cable.GetComponent<Collider>())
            {
                Debug.Log("cable");
                screen2.gameObject.SetActive(true);
                screen0.gameObject.SetActive(false);
                screen1.gameObject.SetActive(false);
                screen3.gameObject.SetActive(false);
                hint2= GetComponent<AudioSource>();
                hint2.PlayDelayed( 4 );
            }
            if (other == modem.GetComponent<Collider>())
            {
                Debug.Log("modem");
                screen3.gameObject.SetActive(true);
                screen0.gameObject.SetActive(false);
                screen2.gameObject.SetActive(false);
                screen1.gameObject.SetActive(false);
                message3= GetComponent<AudioSource>();
                message3.PlayDelayed( 4 );
            }
        }
    }

