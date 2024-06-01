using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edu.Common.Utils
{
    public interface IViewRenderingService
    {
        Task<string> RenderToStringAsync<TModel>(string viewName, TModel model);
    }
}
