using System;
using Xunit;

using base2.Modelo;

namespace ztests.base2.Modelo
{
    public class TestsEntity_SplitJsonObjects
    {
        [Fact]
        public void TestSplitJsonObjects_OK01()
        {
            // Prepara (array de entidades/objetos)
            string jmonedas = "[" + 
                                "{\"MonedaId\":1,\"Simbolo\":\"MU\",\"Nombre\":\"MONEDA UNO\"," +
                                    "\"Tipo\":\"B\",\"TasaCambio\":1.2596}," + 
                                "{\"MonedaId\":2,\"Simbolo\":\"MD\",\"Nombre\":\"MONEDA DOS\"," +
                                    "\"Tipo\":\"X\",\"TasaCambio\":0.9843}," + 
                                "{\"MonedaId\":3,\"Simbolo\":\"MT\",\"Nombre\":\"MONEDA TRES\"," +
                                    "\"Tipo\":\"X\",\"TasaCambio\":9.34}," + 
                                "]";
            string jmoneda1 = "{\"MonedaId\":1,\"Simbolo\":\"MU\",\"Nombre\":\"MONEDA UNO\"," +
                                    "\"Tipo\":\"B\",\"TasaCambio\":1.2596}";
            string jmoneda2 = "{\"MonedaId\":2,\"Simbolo\":\"MD\",\"Nombre\":\"MONEDA DOS\"," +
                                    "\"Tipo\":\"X\",\"TasaCambio\":0.9843}";
            string jmoneda3 = "{\"MonedaId\":3,\"Simbolo\":\"MT\",\"Nombre\":\"MONEDA TRES\"," +
                                    "\"Tipo\":\"X\",\"TasaCambio\":9.34}";

            // Ejecuta
            string[] monedas = Entity.SplitJsonObjects(jmonedas);

            // Comprueba
            Assert.Equal(jmoneda1, monedas[0]);
            Assert.Equal(jmoneda2, monedas[1]);
            Assert.Equal(jmoneda3, monedas[2]);
        }

        [Fact]
        public void TestSplitJsonObjects_OK02()
        {
            // Prepara (array de entidades/objetos)
            string jmonedas = "[" +
                                "{\"MonedaId\":1,\"Simbolo\":\"MU\",\"Nombre\":\"MONEDA UNO\"," +
                                    "\"Tipo\":\"B\",\"TasaCambio\":1.2596}," +
                                "{\"Moneda\":2,\"Nombre\":\"MONEDA DOS\"," +
                                    "\"Tipito\":\"Z\",\"TasaCambio\":0.9843}," +
                                "{\"Mon\":\"3\",\"Simbolo\":\"MT\",\"Name\":\"MONEDA TRES\"," +
                                    "\"Tipo\":\"X\",\"TasaCambio\":9.34}," +
                                "]";
            string jmoneda1 = "{\"MonedaId\":1,\"Simbolo\":\"MU\",\"Nombre\":\"MONEDA UNO\"," +
                                    "\"Tipo\":\"B\",\"TasaCambio\":1.2596}";
            string jmoneda2 = "{\"Moneda\":2,\"Nombre\":\"MONEDA DOS\"," +
                                    "\"Tipito\":\"Z\",\"TasaCambio\":0.9843}";
            string jmoneda3 = "{\"Mon\":\"3\",\"Simbolo\":\"MT\",\"Name\":\"MONEDA TRES\"," +
                                    "\"Tipo\":\"X\",\"TasaCambio\":9.34}";

            // Ejecuta
            string[] monedas = Entity.SplitJsonObjects(jmonedas);

            // Comprueba
            Assert.Equal(jmoneda1, monedas[0]);
            Assert.Equal(jmoneda2, monedas[1]);
            Assert.Equal(jmoneda3, monedas[2]);
        }

        [Fact]
        public void TestSplitJsonObjects_OK03()
        {
            // Prepara (array de objetos)
            string jobjetos = "[" +
                                "{\"MonedaId\":1,\"Simbolo\":\"MU\"}," +
                                "{\"ClienteId\":2,\"Nombre\":\"Rafael Pabon\",\"Telefono\":\"72006319\"}," +
                                "{\"Cuenta\":\"3\",\"Banco\":\"BCP\"}" +
                                "]";
            string jobjeto1 = "{\"MonedaId\":1,\"Simbolo\":\"MU\"}";
            string jobjeto2 = "{\"ClienteId\":2,\"Nombre\":\"Rafael Pabon\",\"Telefono\":\"72006319\"}";
            string jobjeto3 = "{\"Cuenta\":\"3\",\"Banco\":\"BCP\"}";

            // Ejecuta
            string[] objetos = Entity.SplitJsonObjects(jobjetos);

            // Comprueba
            Assert.Equal(jobjeto1, objetos[0]);
            Assert.Equal(jobjeto2, objetos[1]);
            Assert.Equal(jobjeto3, objetos[2]);
        }

        [Fact]
        public void TestSplitJsonObjects_OK04()
        {
            // Prepara (Json array vacío)
            string jobjetos = "[]";

            // Ejecuta
            string[] objetos = Entity.SplitJsonObjects(jobjetos);

            // Comprueba
            Assert.Empty(objetos);
        }

        [Fact]
        public void TestSplitJsonObjects_OK05()
        {
            // Prepara (Json array = objeto vacío)
            string jobjetos = "{}";

            // Ejecuta
            string[] objetos = Entity.SplitJsonObjects(jobjetos);

            // Comprueba
            Assert.Equal("{}", objetos[0]);
        }

        [Fact]
        public void TestSplitJsonObjects_ZBad01()
        {
            // Prepara (null Json array)
            string jobjetos = null;

            // Ejecuta
            try
            {
                Assert.Throws<NullReferenceException>(() => Entity.SplitJsonObjects(jobjetos));
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }

        [Fact]
        public void TestSplitJsonObjects_ZBad02()
        {
            // Prepara (empty Json array)
            string jobjetos = "";

            // Ejecuta
            try
            {
                Assert.Throws<NullReferenceException>(() => Entity.SplitJsonObjects(jobjetos));
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }

        [Fact]
        public void TestSplitJsonObjects_ZBad03()
        {
            // Prepara (espacio Json array)
            string jobjetos = " ";

            // Ejecuta
            try
            {
                Assert.Throws<NullReferenceException>(() => Entity.SplitJsonObjects(jobjetos));
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }

        [Fact]
        public void TestSplitJsonObjects_ZBad04()
        {
            // Prepara (Json array basura)
            string jobjetos = "abc";

            // Ejecuta
            try
            {
                Assert.Throws<ApplicationException>(() => Entity.SplitJsonObjects(jobjetos));
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }
    }
}
