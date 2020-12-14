using InternTask1.Models;
using System.Collections.Generic;

namespace InternTask1.Services.Abstract
{
    public interface IRepository
    {
        void Save(IEnumerable<Website> sites);
    }
}
