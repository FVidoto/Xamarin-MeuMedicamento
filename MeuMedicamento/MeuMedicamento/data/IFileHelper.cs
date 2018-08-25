using System;
using System.Collections.Generic;
using System.Text;

namespace MeuMedicamento.data
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
