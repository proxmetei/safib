using System;
namespace service_B.Models.Exceptions
{
    /// <summary>
    ///  Класс NotEnoughtDataException
    ///  класс отвечает за ошибку в случае отсутсвия подразделения,
    ///  в которое надо войти
    /// </summary>
    [Serializable]
    public class NoSuchIncluddedException : Exception
    {
        public string Content { get; }
        public int Included_id { get; }
        public NoSuchIncluddedException() { }
        public NoSuchIncluddedException(string message)
            : base(message) { }
        public NoSuchIncluddedException(string message, Exception inner)
            : base(message, inner) { }
    }
}
