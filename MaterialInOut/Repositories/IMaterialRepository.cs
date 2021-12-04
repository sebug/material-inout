using System;
namespace MaterialInOut.Repositories
{
    public interface IMaterialRepository
    {
        void ImportExcelFile(byte[] bytes);
    }
}

