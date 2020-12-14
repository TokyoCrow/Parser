using System.Text;

namespace InternTask1.Services.Abstract
{
    interface ISendEmail
    {
        void Send(StringBuilder mailText);
    }
}
