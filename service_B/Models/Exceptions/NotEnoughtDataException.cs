using System;
namespace service_B.Models.Exceptions
{
    /// <summary>
    ///  Класс NotEnoughtDataException
    ///  класс отвечает за ошибку в случае недостака данных из файла
    /// </summary>
    [Serializable]
    public class NotEnoughtDataException: Exception
    {
        public string Content { get; }
        public int Included_id { get; }
        public NotEnoughtDataException() { }
        public NotEnoughtDataException(string message)
            : base(message) { }
        public NotEnoughtDataException(string message, Exception inner)
            : base(message, inner) { }
    }
}
