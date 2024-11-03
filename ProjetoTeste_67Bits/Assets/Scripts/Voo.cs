using UnityEngine;

public class Voo : Movimento
{
    private float _velocidadeDeMovimento = 100f;

    private bool _podeRotacionar = true;

    public float VelocidadeDeMovimento { get => _velocidadeDeMovimento; set => _velocidadeDeMovimento = value; }

    protected override void Start()
    {
        base.Start();

        _rb.AddForce(Vector3.forward * 1000f * Time.deltaTime, ForceMode.Impulse);
    }

    void Update()
    {
        _rb.AddForce(Vector3.forward * _velocidadeDeMovimento * Time.deltaTime);

        if (transform.rotation.z > 0.1f || transform.rotation.z < -0.1f)
            _podeRotacionar = false;
        else
            _podeRotacionar = true;

        if (_podeRotacionar)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _rb.AddForce(Vector3.left * _velocidadeLateral * Time.deltaTime, ForceMode.VelocityChange);
                _rb.AddTorque(Vector3.forward * _velocidadeDeRotacao * Time.deltaTime);
                return;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _rb.AddForce(Vector3.left * -_velocidadeLateral * Time.deltaTime, ForceMode.VelocityChange);
                _rb.AddTorque(Vector3.forward * -_velocidadeDeRotacao * Time.deltaTime);
                return;
            }
        }

        Tremer();
    }
}