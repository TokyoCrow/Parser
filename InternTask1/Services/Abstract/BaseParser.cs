using InternTask1.Models;
using System.Collections.Generic;
using System.Text;

namespace InternTask1.Services.Abstract
{
    public abstract class BaseParser
    {
        public string Url { get; set; }
        public abstract StringBuilder Errors { get; }
        public abstract void Initialize();
        public abstract IEnumerable<Website> ShieldedUrls();
    }
}
