namespace RedditMockup.Common.Dtos;

public class UserDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Family { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public ProfileDto? Profile { get; set; }

    public ICollection<string>? Roles { get; set; }
}