
using API_SBI.Entities;
using API_SBI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SBI.Services
{
    public class ServerPostService
    {
        private readonly IServerPost _IServerPostRepository;
        public ServerPostService(IServerPost ServerPostRepository)
        {
            _IServerPostRepository = ServerPostRepository;
        }

        public ICollection<ServerPost> GetAllServerPost()
        {
            return _IServerPostRepository.GetAllServerPost();
        }
        public ServerPost GetServerPostbyId(int id)
        {
            return _IServerPostRepository.GetServerPostById(id);
        }
    }
}
