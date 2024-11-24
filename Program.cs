namespace SistemaEnviosStrategyPattern
{
    public enum CategoriaDeDistancia
    {
        Local,
        Regional,
        Nacional,
        Internacional,
    }

    public enum VelocidadDeServicio
    {
        Economico,
        Estandar,
        MismoDia,
    }

    interface IEstrategiaDeEnvio
    {
        double CalcularCostoDeEnvio(
            Paquete paquete,
            CategoriaDeDistancia distancia,
            VelocidadDeServicio velocidad
        );
    }




    //Contexto
    class SistemaDeEnvios
    {
        private IEstrategiaDeEnvio _estrategia;

        public SistemaDeEnvios(IEstrategiaDeEnvio estr)
        {
            _estrategia = estr;
        }

        public double CalcularCostoDeEnvio(
            Paquete paquete,
            CategoriaDeDistancia distancia,
            VelocidadDeServicio velocidad
        )
        {
            return _estrategia.CalcularCostoDeEnvio(paquete, distancia, velocidad);
        }
    }




    public class Paquete
    {
        public double Peso;
        public string Tamaño;

        public Paquete(double peso, string tamaño)
        {
            Peso = peso;
            Tamaño = tamaño;
        }
    }




    public class EnvioLocalStandar : IEstrategiaDeEnvio
    {
        public double CalcularCostoDeEnvio(
            Paquete paquete,
            CategoriaDeDistancia distancia,
            VelocidadDeServicio velocidad
        )
        {
            double costoBase = 90;

            if (paquete.Peso > 50)
            {
                costoBase = 10.0;
            }
            else if (
                distancia == CategoriaDeDistancia.Local
                && velocidad == VelocidadDeServicio.Estandar
            )
            {
                costoBase = 20.0;
            }
            return costoBase;
        }
    }




    class Program
    {
        static void Main()
        {
            // 1. Crear una instancia de la estrategia de envío (EnvioLocalStandar)
            IEstrategiaDeEnvio envioLocalStandar = new EnvioLocalStandar();

            // 2. Crear una instancia de SistemaDeEnvios con la estrategia seleccionada
            SistemaDeEnvios sistemaDeEnvios = new SistemaDeEnvios(envioLocalStandar);
            Paquete paquete = new Paquete(30, "Pequeño");

            // 4. Llamar al método CalcularCostoDeEnvio y mostrar el resultado
            double costoEnvio = sistemaDeEnvios.CalcularCostoDeEnvio(
                paquete,
                CategoriaDeDistancia.Local,
                VelocidadDeServicio.Estandar
            );

            // 5. Mostrar el costo de envío calculado
            Console.WriteLine($"El costo del envío es: {costoEnvio}");
        }
    }
}
