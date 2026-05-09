using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakery.DTO
{
     public interface IManage
    {
        void LoadData();
        void Create();
        void Delete();
        void Edit();
        void Search();
        void SaveToFile();
    }
}
