namespace asp_net_core.Models
{
    public interface ILugar
    {
       string Dirección { get; set; }

       void LimpiarLugar();

    }
}