using System;
using MaterialInOut.Models;

namespace MaterialInOut.Repositories
{
    public interface IMaterialRepository
    {
        void ImportExcelFile(byte[] bytes);

        IEnumerable<MaterialItem> GetItems();
    }
}

