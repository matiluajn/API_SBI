using API_SBI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SBI.Repository.IRepository
{
    public interface IServerPost 
    {    

        ICollection<ServerPost> GetAllServerPost();        

        ServerPost GetServerPostById(int id);
    }
}
