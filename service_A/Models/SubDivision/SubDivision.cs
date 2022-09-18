using System;
namespace service_A.Models.SubDivision
{
	/// <summary>
	///  Класс SubDivision
	///  отвечает за модель подразделения
	/// </summary>
	public class SubDivision
    {
		public int Id { get; set; }

		public bool Status { get; set; }

		public bool changeState() {
			Status = !Status;
			return Status;
		}
		public void setState() {
			Random rnd = new Random();
			if (rnd.Next(0, 2) == 0)
			{
				Status = false;
			}
			else
			{
				Status = true;
			}
		}
	   
    }
}
