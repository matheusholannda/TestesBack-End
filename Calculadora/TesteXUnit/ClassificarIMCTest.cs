using Calculadora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteXUnit
{
    public class ClassificarIMCTest
    {
        [Fact]
        public void ClassificarIMC_RetornaResultado()
        {
            double imc = 28;

            var resultado = Operacoes.ClassificarIMC(imc);

            Assert.Equal("Sobrepeso", resultado);
        }

        [Theory]
        [InlineData(28, "Sobrepeso")]
        [InlineData(31, "Obesidade Grau I")]
        public void ClassificarIMC_RetornaResultado_ParaUmaListaDeValores(double imc, string resultadoEsperado)
        {
            var resultadoIMC = Operacoes.ClassificarIMC(imc);

            Assert.Equal(resultadoEsperado, resultadoIMC);
        }
    }
}
