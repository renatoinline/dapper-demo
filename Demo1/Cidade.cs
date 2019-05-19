using System;
using System.Collections.Generic;
using System.Text;

namespace Demo1
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public Estado Estado { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Nome} ({Tipo}), {Estado}";
        }
    }
}
