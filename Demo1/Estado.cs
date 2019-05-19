using System;
using System.Collections.Generic;
using System.Text;

namespace Demo1
{
    public class Estado
    {
        public int Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Nome}-{Sigla}";
        }
    }
}
