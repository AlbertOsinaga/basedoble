using System;
using Xunit;

using base2.Modelo;

namespace ztests.base2.Modelo
{
    public class TestsEntity_FromJson
    {
        [Fact]
        public void TestFromJson_OK01()
        {
            // Prepara (objeto completo)
            Moneda moneda = new Moneda();
            string jmoneda = "{\"MonedaId\":1,\"Simbolo\":\"MU\",\"Nombre\":\"MONEDA UNO\"," + 
                                "\"Tipo\":\"B\",\"TasaCambio\":1.2596}";

            // Ejecuta
            moneda.FromJson(jmoneda);

            // Comprueba
            Assert.Equal(1, moneda.MonedaId);
            Assert.Equal("MU", moneda.Simbolo);
            Assert.Equal("MONEDA UNO", moneda.Nombre);
            Assert.Equal("B", moneda.Tipo);
            Assert.Equal(1.2596M, moneda.TasaCambio);
        }

        [Fact]
        public void TestFromJson_OK02()
        {
            // Prepara (objeto sin nombre, string null por defecto)
            Moneda moneda = new Moneda();
            moneda.Nombre = "Anterior Nombre";
            string jmoneda = "{\"MonedaId\":1,\"Simbolo\":\"MU\"," +
                                "\"Tipo\":\"B\",\"TasaCambio\":1.2596}";

            // Ejecuta
            moneda.FromJson(jmoneda);

            // Comprueba
            Assert.Equal(1, moneda.MonedaId);
            Assert.Equal("MU", moneda.Simbolo);
            Assert.Null(moneda.Nombre);
            Assert.Equal("B", moneda.Tipo);
            Assert.Equal(1.2596M, moneda.TasaCambio);
        }

        [Fact]
        public void TestFromJson_OK03()
        {
            // Prepara (moneda sin MonedaId, entero 0 por defecto)
            Moneda moneda = new Moneda();
            moneda.MonedaId = 22;
            string jmoneda = "{\"Simbolo\":\"MU\",\"Nombre\":\"MONEDA UNO\"," +
                                "\"Tipo\":\"B\"}";

            // Ejecuta
            moneda.FromJson(jmoneda);

            // Comprueba
            Assert.Equal(0, moneda.MonedaId);
            Assert.Equal("MU", moneda.Simbolo);
            Assert.Equal("MONEDA UNO", moneda.Nombre);
            Assert.Equal("B", moneda.Tipo);
            Assert.Equal(0M, moneda.TasaCambio);
        }

        [Fact]
        public void TestFromJson_OK04()
        {
            // Prepara (objeto Json vacío)
            Moneda moneda = new Moneda();
            moneda.Simbolo = "AA";
            string jmoneda = "{}";

            // Ejecuta
            moneda.FromJson(jmoneda);

            // Comprueba
            Assert.Equal(0, moneda.MonedaId);
            Assert.Null(moneda.Simbolo);
            Assert.Null(moneda.Nombre);
            Assert.Null(moneda.Tipo);
            Assert.Equal(0M, moneda.TasaCambio);
        }

        [Fact]
        public void TestFromJson_OK05()
        {
            // Prepara (moneda Json con campos errados "NoEXiste")
            Moneda moneda = new Moneda();
            string jmoneda = "{\"MonedaId\":20,\"NoExiste\":0}";

            // Ejecuta
            moneda.FromJson(jmoneda);

            // Comprueba
            Assert.Equal(20, moneda.MonedaId);
            Assert.Null(moneda.Simbolo);
            Assert.Null(moneda.Nombre);
            Assert.Null(moneda.Tipo);
            Assert.Equal(0M, moneda.TasaCambio);
        }

        [Fact]
        public void TestFromJson_ZBad01()
        {
            // Prepara (moneda nula)
            Moneda moneda = new Moneda();
            string jmoneda = null;

            // Ejecuta
            try
            {
                Assert.Throws<ApplicationException>(() => { moneda.FromJson(jmoneda); });   // idem moneda.FromJson()
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }

        [Fact]
        public void TestFromJson_ZBad02()
        {
            // Prepara (moneda empty)
            Moneda moneda = new Moneda();
            string jmoneda = "";

            // Ejecuta
            try
            {
                Assert.Throws<ApplicationException>(() => { moneda.FromJson(jmoneda); });
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }

        [Fact]
        public void TestFromJson_ZBad03()
        {
            // Prepara (moneda no Json)
            Moneda moneda = new Moneda();
            string jmoneda = "abc";

            // Ejecuta
            try
            {
                Assert.Throws<ApplicationException>(() => { moneda.FromJson(jmoneda); });
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }

        [Fact]
        public void TestFromJson_ZBad04()
        {
            // Prepara (moneda Json con errores de formato)
            Moneda moneda = new Moneda();
            string jmoneda = "{\"MonedaId\"=20}";

            // Ejecuta
            try
            {
                Assert.Throws<ApplicationException>(() => { moneda.FromJson(jmoneda); });
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }
    }
}
