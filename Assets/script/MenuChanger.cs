using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuChanger : MonoBehaviour
{

    public void Tomycontractworld() => SceneManager.LoadScene("PlayerNFTContract Scene");
    public void Tomyworld() => SceneManager.LoadScene("Player Scene");
    public void contractworld() => SceneManager.LoadScene("NFTContract Scene");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
