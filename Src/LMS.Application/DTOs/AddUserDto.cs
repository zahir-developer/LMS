namespace LMS.Application.DTOs
{
    public class AddUserDto
    {
        public int? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
    }
}