namespace TUNIWEB.Models
{
    public class catalogoCarrerasTecnicas
    {
        public int carreTecnicaId { get; set; }
        public string carreraTecnica { get; set; }
        public carreraTecnica relcart_Cat { get; set; }
    }
}
