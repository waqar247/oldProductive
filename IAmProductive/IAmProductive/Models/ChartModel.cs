using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmProductive.Models
{
    public class ChartModel
    {
        public string Name { get; set; }

        public double Height { get; set; }
        public ChartModel(string TaskType,double height)
        {
            this.Name = TaskType;
            this.Height = height;
        }
    }
}
