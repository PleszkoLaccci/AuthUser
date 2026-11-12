namespace AuthUserAPI.Models.DTOS
{
    public class AddRolesToUserDTO
    {
        public Guid UserId { get; set; }
        public Guid RolesId { get; set; }

    }
}
