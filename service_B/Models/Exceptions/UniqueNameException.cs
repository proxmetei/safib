using System;
namespace service_B.Models.Exceptions
{
    /// <summary>
    ///  Класс UniqueNameException
    ///  класс отвечает за ошибку в случае повторения имени подразделения
    /// </summary>
    [Serializable]
    public class UniqueNameException : Exception
    {
        public string Name { get; }
        public UniqueNameException(){}
        public UniqueNameException(string message)
            : base(message) { }
        public UniqueNameException(string message, Exception inner)
            : base(message, inner) { }
        public UniqueNameException(string message, string name)
            : this(message) {
            Name = name;
        }
    }
}
