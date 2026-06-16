using RestaurantOS.Domain.Enums;

namespace RestaurantOS.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public RolUsuario Rol {  get; set; }
        public bool Activo { get; set; } = true;
    }
}
