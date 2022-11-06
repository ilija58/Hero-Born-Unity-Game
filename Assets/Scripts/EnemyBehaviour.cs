using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour, IShootable
{
    public Transform player;

    public Transform patrolRoute; //���������� ��� ��������� ������� � ������� ����������� �����
    public List<Transform> locations; //������ ����� �����������

    public GameObject bullet;
    public float bulletSpeed = 100.0f;

    private int locationIndex = 0; //������� �������
    private NavMeshAgent agent; //������-����

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

    private void Start() // �������, ������� ���������� ����� ������� ����
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>(); //��������� ���������� �����
        InitializePatrolRoute(); //����� ������� ���������� ������ <locations> �������
        MoveToNextPatrolLocation(); //����� ������� �������� �����
    }

    void Update() // �������, ������� ���������� ������ ���� 
    {
        if(agent.remainingDistance < 0.2f && !agent.pathPending)// �������� �������: 1. �������� �� �� ����� ������ 0.2; 2. ����������� �� ������� ��� ���. 
        {
            MoveToNextPatrolLocation();// ������ ������ �������
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
        foreach(Transform child in patrolRoute)//������������� ��������� foreach ��� ������ ����� �� ������� <patrolRoute> � ������ <locations>
        {
            locations.Add(child); // ���������� ����� ����� � ������ <locations>
        }
    }
    void MoveToNextPatrolLocation()
    {
        if(locations.Count == 0) // ���� ���-�� ������� ����� 0, �� ����� �� �������, ����������� � ����������
            return;
        agent.destination = locations[locationIndex].position; // ������������ ����� ������� ������� <agent> �� ������ <locations>
        locationIndex = (locationIndex + 1) % locations.Count; // �������� ��������� ������� ��� �����
    }

    private void OnTriggerEnter(Collider other) // ������� ��� ����������� �������� � ���� ��������� �����
    {
        if(other.name == "Player") // �������� �������, �������� �� ��� ��� ����� � ���� ��������� �������
        {
            agent.destination = player.position;
            
            Shoot();
            Debug.Log("Player detected - attack!"); // ����� ���������
        }
        
    }

    private void OnTriggerExit(Collider other) // ������� ��� ����������� �������� � ���� ��������� �����
    {
        if(other.name == "Player") // �������� �������, �������� �� ��� ��� ����� �� ���� ��������� �������
        {
            Debug.Log("Player out of range - resume patrol"); // ����� ���������
        }
    }

}
