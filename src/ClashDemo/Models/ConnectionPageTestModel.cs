using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.Models
{
    [AddINotifyPropertyChangedInterface]
    public class ConnectionPageTestModel
    {
        [Column("姓名")]
        public string Name { get; set; }
        [Column("年龄")]
        public int Age { get; set; }

        [Column("薪资")]
        public double Salary { get; set; }

        [Column("部门")]
        public string Part { get; set; }
        [Column("ID")]
        public int ID { get; set; }
    }
}
