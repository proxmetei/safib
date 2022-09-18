namespace service_B.Models.SubDivision
{
    /// <summary>
    ///  Класс SubDivision
    ///  отвечает за модель подразделения
    /// </summary>
    public class SubDivision
    {
        public int Id { get; set; }

        public bool? Status { get; set; }

        public string Name { get; set; }

        public int? Included_in_Id { get; set; }

        public SubDivision Included_in { get; set; }
    }
}
