namespace Kursa4.DAL.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        : base() { }

        public NotFoundException(string message)
            : base(message) { }

        public NotFoundException(string name, object key)
            : base($"Сущность \"{name}\" с ключом({key}) не найдена.") { }
    }
}
