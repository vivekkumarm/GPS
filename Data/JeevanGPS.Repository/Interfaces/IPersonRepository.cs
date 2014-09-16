using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanGPS.Repository.Interfaces
{
    public interface IPersonRepository
    {
        void Insert(JeevanGPS.BO.Person person);        
    }
}
