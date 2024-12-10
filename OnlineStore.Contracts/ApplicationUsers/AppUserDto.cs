namespace OnlineStore.Contracts.ApplicationUsers
{
    public class AppUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
    }
}
