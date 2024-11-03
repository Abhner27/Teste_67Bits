using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Movimento : MonoBehaviour
{
    protected Rigidbody _rb;

    protected float _velocidadeDeRotacao = 50f;
    protected float _velocidadeLateral = 4f;

    public float VelocidadeDeRotacao { get => _velocidadeDeRotacao; set => _velocidadeDeRotacao = value; }
    public float VelocidadeLateral { get => _velocidadeLateral; set => _velocidadeLateral = value; }

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
    }

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddTorque(Vector3.forward * -_velocidadeDeRotacao * Time.deltaTime);
    }

    protected void Tremer()
    {
        if (transform.rotation.z > 0)
            _rb.AddTorque(Vector3.forward * -_velocidadeDeRotacao * Time.deltaTime);
        else if (transform.rotation.z < 0)
            _rb.AddTorque(Vector3.forward * _velocidadeDeRotacao * Time.deltaTime);
    }
}