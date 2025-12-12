using System.ComponentModel.DataAnnotations;

namespace Finance.Shared.Entities;

public class User
{
    private string _login = string.Empty;
    [Key]
    public int Id { get; private set; }

    [Required(ErrorMessage = "O campo login é obrigatório", AllowEmptyStrings = false)]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo login deve ter no mínimo 3 caracteres")]
    // [RegularExpression(@"^\S{3,}$", ErrorMessage = "O campo não pode ter apenas espaços em branco")]
    public string Login
    {
        get => _login;
        set => _login = value.Trim();
        // set => SetLogin(value);
    }

    [Required(AllowEmptyStrings = false)]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo senha é obrigatório", AllowEmptyStrings = false)]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo senha deve ter no mínimo 3 caracteres")]
    public string Password { get; set; } = string.Empty;

    public bool IsActive { get; set; } = false;

    // private void SetLogin(string value) => _login = value.Trim();
}
