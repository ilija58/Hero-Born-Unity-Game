using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour, IShootable
{
    public Transform player;

    public Transform patrolRoute; //Переменная для получения объекта с точками перемещения врага
    public List<Transform> locations; //Список точек перемещения

    public GameObject bullet;
    public float bulletSpeed = 100.0f;

    private int locationIndex = 0; //Текущая локация
    private NavMeshAgent agent; //Объект-враг

    private int _lives = 3;
    
    public int EnemyLives
    {
        get
        {
            return _lives;
        }
        private set
        {
            _lives = value;

            if(_lives < 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    private void Start() // Функция, которая вызывается перед стартом игры
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>(); //Получение компонента врага
        InitializePatrolRoute(); //Вызов функции заполнения списка <locations> точками
        MoveToNextPatrolLocation(); //Вызов функции движения точке
    }

    void Update() // Функция, которая вызывается каждый кадр 
    {
        if(agent.remainingDistance < 0.2f && !agent.pathPending)// Проверка условий: 1. Осталось ли до точки меньше 0.2; 2. Вычисляется ли маршрут или нет. 
        {
            MoveToNextPatrolLocation();// Повтор вызова функции
        }
    }
    public void GetDamage()
    {
        EnemyLives = -1;
    }

    public void Shoot()
    {
        GameObject newEnemyBullet = Instantiate(bullet, this.transform.position + new Vector3(0, 0, 0.2f), this.transform.rotation) as GameObject;
        Rigidbody BulletRB = newEnemyBullet.GetComponent<Rigidbody>();
        BulletRB.velocity = this.transform.forward * bulletSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            GetDamage();
            Debug.Log("Critical hit!");
        }
    }

    void InitializePatrolRoute()
    {
        foreach(Transform child in patrolRoute)//Использование итератора foreach для записи точек из объекта <patrolRoute> в список <locations>
        {
            locations.Add(child); // Добавление новой точки в список <locations>
        }
    }
    void MoveToNextPatrolLocation()
    {
        if(locations.Count == 0) // Если кол-во локаций равно 0, то выход из функции, прекращение её выполнения
            return;
        agent.destination = locations[locationIndex].position; // Присваивание новой позиции объекту <agent> из списка <locations>
        locationIndex = (locationIndex + 1) % locations.Count; // Пересчёт следующей позиции для врага
    }

    private void OnTriggerEnter(Collider other) // Функция для обнаружения объектов в зоне видимости врага
    {
        if(other.name == "Player") // Проверка условия, является ли тот кто вошёл в зону видимости игроком
        {
            agent.destination = player.position;
            
            Shoot();
            Debug.Log("Player detected - attack!"); // Вывод сообщения
        }
        
    }

    private void OnTriggerExit(Collider other) // Функция для обнаружения объектов в зоне видимости врага
    {
        if(other.name == "Player") // Проверка условия, является ли тот кто вышел из зоны видимости игроком
        {
            Debug.Log("Player out of range - resume patrol"); // Вывод сообщения
        }
    }

}
