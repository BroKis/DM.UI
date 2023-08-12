using System.ComponentModel.DataAnnotations;

namespace DM.UI.Models;

public class AuthModel
{
    [Required]
    [Display(Name = "Логин")]
    public string Login { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    [StringLength(100, ErrorMessage = 
        "Поле {0} должно иметь минимум {2} и максимум {1} символов", MinimumLength = 5)]
    public string Password { get; set; } = string.Empty;
}