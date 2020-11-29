using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NETCore_Demo.Models
{
	public class Employee
	{
		[Key]
		public int EmployeeId { get; set; }

		[Column(TypeName = "nvarchar(250)")]
		[Required(ErrorMessage = "名称为必填项")]
		[DisplayName("名称")]
		public string FullName { get; set; }

		[Column(TypeName = "varchar(10)")]
		[DisplayName("编号")]
		public string EmpCode { get; set; }

		[Column(TypeName = "varchar(100)")]
		[DisplayName("位置")]
		public string Position { get; set; }

		[Column(TypeName = "varchar(100)")]
		[DisplayName("坐标")]
		public string OfficeLocation { get; set; }

	}
}
