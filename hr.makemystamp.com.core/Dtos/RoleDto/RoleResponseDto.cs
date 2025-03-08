namespace hr.makemystamp.com.core.Dtos.RoleDto
{
    public record RoleResponseDto
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public record RoleResponse
    {
        public int count { get; set; }
        public List<RoleResponseDto> items { get; set; }
    }
}
