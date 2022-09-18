using System;
namespace service_B.Models.Exceptions
{
    /// <summary>
    ///  Класс SubdivisionTreeException
    ///  класс отвечает за ошибку в случае появления цикла в дереве
    /// </summary>
    [Serializable]
    public class SubdivisionTreeException: Exception
    {
        public string Name { get; }
        public string Included_name { get; }
        public SubdivisionTreeException(){ }
        public SubdivisionTreeException(string message)
            : base(message) { }
        public SubdivisionTreeException(string message, Exception inner)
            : base(message, inner) { }
        public SubdivisionTreeException(string message, string name, string included_name)
            : this(message)
        {
            Name = name;
            Included_name = included_name;
        }
    }
}
