using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour
{

    private GameObject _floor;
    private GameObject _templateCoin;
    private Dictionary<GameObject, bool> _coins = new Dictionary<GameObject, bool>();

    public int CoinsAmount = 15;

    // Start is called before the first frame update
    void Start()
    {
        _floor = GameObject.FindGameObjectWithTag("Floor");
        _templateCoin = GameObject.FindGameObjectWithTag("Coin");
        
        SpawnCoins();
    }

    void SpawnCoins()
    {
        var MAX_PLANE_COORDINATE = 27;
        for (var i = 0; i < CoinsAmount; i++)
        {
            GameObject coin = Instantiate(_templateCoin);
            coin.transform.position = new Vector3(Random.Range(-MAX_PLANE_COORDINATE, MAX_PLANE_COORDINATE),
                1, Random.Range(-MAX_PLANE_COORDINATE, MAX_PLANE_COORDINATE));
            
            _coins.Add(coin, false);
            
        }
        
        // Delete the template.
        Destroy(_templateCoin);
    }

    public bool PickUpCoin(GameObject gObject)
    {
        if (!_coins[gObject])
        {
            _coins[gObject] = true;
            gObject.transform.localScale = new Vector3(0, 0, 0);
            return true;
        }

        return false;

    }

    public int GetCoins()
    {
        return _coins.Values.ToList().FindAll(val => val).Count;
    }
}
