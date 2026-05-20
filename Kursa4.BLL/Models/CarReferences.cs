using System.Collections.Generic;

namespace Kursa4.BLL.Models
{
    public static class CarReferences
    {
        public static readonly List<string> EngineTypes = new()
        {
            "Бензин",
            "Дизель",
            "Электро",
            "Гибрид",
            "Газ"
        };

        public static readonly List<string> BodyTypes = new()
        {
            "Седан",
            "Хэтчбек",
            "Универсал",
            "Внедорожник",
            "Кроссовер",
            "Купе",
            "Кабриолет",
            "Минивэн",
            "Пикап",
            "Лимузин",
            "Фургон"
        };

        public static readonly List<string> Drives = new()
        {
            "Передний",
            "Задний",
            "Полный"
        };
    }
}
