using BLL.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BLL
{
    public class Students : IStudents
    {
        public void SetStudents(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream stream = new MemoryStream(bytes);
        }
    }
}
