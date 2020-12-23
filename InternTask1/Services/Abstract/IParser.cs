using InternTask1.Models;
using System.Collections.Generic;
using System.Text;

namespace InternTask1.Services.Abstract
{
    public interface IParser
    {
        StringBuilder Errors { get; }
        void Initialize(string url);
        IEnumerable<Website> ShieldedUrls();
    }
}
