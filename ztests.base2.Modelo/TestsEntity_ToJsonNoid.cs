using System;
using Xunit;

using base2.Modelo;

namespace ztests.base2.Modelo
{
    public class TestsEntity_ToJsonNoid
    {
        [Fact]
        public void TestToJsonNoid_OK01()
        {
            // Prepara (string json fields nulo)
            string jfields = null;
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X",
                TasaCambio = 3.4783M
            };
            string jmonedaEsperada = "{\"Simbolo\":\"MUN\",\"Nombre\":\"Moneda Unica\"," +
                                        "\"Tipo\":\"X\",\"TasaCambio\":3.4783}";

            // Ejecuta
            string jmoneda = moneda.ToJsonNoid(jfields);    // idem moneda.ToJsonNoid()

            // Comprueba
            Assert.Equal(jmonedaEsperada, jmoneda);
        }


        [Fact]
        public void TestToJsonNoid_OK02()
        {
            // Prepara (string json fields empty)
            string jfields = "";
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X",
                TasaCambio = 3.4783M
            };
            string jmonedaEsperada = "{\"Simbolo\":\"MUN\",\"Nombre\":\"Moneda Unica\"," +
                                        "\"Tipo\":\"X\",\"TasaCambio\":3.4783}";

            // Ejecuta
            string jmoneda = moneda.ToJsonNoid(jfields);

            // Comprueba
            Assert.Equal(jmonedaEsperada, jmoneda);
        }

        [Fact]
        public void TestToJsonNoid_OK03()
        {
            // Prepara (string json fields nulo, campos en otro orden)
            string jfields = null;
            Moneda moneda = new Moneda
            {
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X",
                MonedaId = 1,
                TasaCambio = 3.4783M
            };
            string jmonedaEsperada = "{\"Simbolo\":\"MUN\",\"Nombre\":\"Moneda Unica\"," +
                                        "\"Tipo\":\"X\",\"TasaCambio\":3.4783}";

            // Ejecuta
            string jmoneda = moneda.ToJsonNoid(jfields);    // idem moneda.ToJsonNoid()

            // Comprueba
            Assert.Equal(jmonedaEsperada, jmoneda);
        }
        [Fact]

        public void TestToJsonNoid_OK04()
        {
            // Prepara (string json fields nulo, numericos por defecto)
            string jfields = null;
            Moneda moneda = new Moneda
            {
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X"
            };
            string jmonedaEsperada = "{\"Simbolo\":\"MUN\",\"Nombre\":\"Moneda Unica\"," +
                                        "\"Tipo\":\"X\",\"TasaCambio\":0.0}";

            // Ejecuta
            string jmoneda = moneda.ToJsonNoid(jfields);    // idem moneda.ToJsonNoid()

            // Comprueba
            Assert.Equal(jmonedaEsperada, jmoneda);
        }

        [Fact]
        public void TestToJsonNoid_OK05()
        {
            // Prepara (string json fields nulo, strings por defecto)
            string jfields = null;
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                TasaCambio = 3.4783M
            };
            string jmonedaEsperada = "{\"Simbolo\":null,\"Nombre\":null," +
                                        "\"Tipo\":null,\"TasaCambio\":3.4783}";

            // Ejecuta
            string jmoneda = moneda.ToJsonNoid(jfields);    // idem moneda.ToJsonNoid()

            // Comprueba
            Assert.Equal(jmonedaEsperada, jmoneda);
        }

        [Fact]
        public void TestToJsonNoid_OK06()
        {
            // Prepara (jfields Id y algunos)
            string jfields = "[\"MonedaId\",\"Nombre\",\"TasaCambio\"]";
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X",
                TasaCambio = 3.4783M
            };
            string jmonedaEsperada = "{\"MonedaId\":1,\"Nombre\":\"Moneda Unica\"," +
                                        "\"TasaCambio\":3.4783}";

            // Ejecuta
            string jmoneda = moneda.ToJsonNoid(jfields);

            // Comprueba
            Assert.Equal(jmonedaEsperada, jmoneda);
        }

        [Fact]
        public void TestToJsonNoid_OK07()
        {
            // Prepara (jfields no Id y algunos)
            string jfields = "[\"Nombre\",\"TasaCambio\"]";
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X",
                TasaCambio = 3.4783M
            };
            string jmonedaEsperada = "{\"Nombre\":\"Moneda Unica\"," +
                                        "\"TasaCambio\":3.4783}";

            // Ejecuta
            string jmoneda = moneda.ToJsonNoid(jfields);

            // Comprueba
            Assert.Equal(jmonedaEsperada, jmoneda);
        }

        [Fact]
        public void TestToJsonNoid_OK08()
        {
            // Prepara (jfields solo Id)
            string jfields = "[\"MonedaId\"]";
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X",
                TasaCambio = 3.4783M
            };
            string jmonedaEsperada = "{\"MonedaId\":1}";

            // Ejecuta
            string jmoneda = moneda.ToJsonNoid(jfields);

            // Comprueba
            Assert.Equal(jmonedaEsperada, jmoneda);
        }

        [Fact]
        public void TestToJsonNoid_OK09()
        {
            // Prepara (string json fields con objeto vacio)
            string jfields = "{}";
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X",
                TasaCambio = 3.4783M
            };
            string jmonedaEsperada = "{}";

            // Ejecuta
            string jmoneda = moneda.ToJsonNoid(jfields);

            // Comprueba
            Assert.Equal(jmonedaEsperada, jmoneda);
        }

        [Fact]
        public void TestToJsonNoid_OK10()
        {
            // Prepara (string json fields con array vacio)
            string jfields = "[]";
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X",
                TasaCambio = 3.4783M
            };
            string jmonedaEsperada = "{}";

            // Ejecuta
            string jmoneda = moneda.ToJsonNoid(jfields);

            // Comprueba
            Assert.Equal(jmonedaEsperada, jmoneda);
        }

        [Fact]
        public void TestToJsonNoid_OK11()
        {
            // Prepara (jfields Id y algunos con case mixed)
            string jfields = "[\"nombre\",\"tasaCaMbio\"]";
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X",
                TasaCambio = 3.4783M
            };
            string jmonedaEsperada = "{\"Nombre\":\"Moneda Unica\"," +
                                        "\"TasaCambio\":3.4783}";

            // Ejecuta
            string jmoneda = moneda.ToJsonNoid(jfields);

            // Comprueba
            Assert.Equal(jmonedaEsperada, jmoneda);
        }

        [Fact]
        public void TestToJsonNoid_ZBad01()
        {
            // Prepara (string json fields basura)
            string jfields = "abc";
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X",
                TasaCambio = 3.4783M
            };

            // Ejecuta
            try
            {
                Assert.Throws<ApplicationException>(() => { moneda.ToJsonNoid(jfields); });
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }

        [Fact]
        public void TestToJsonNoid_ZBad02()
        {
            // Prepara (string json fields con error de formato)
            string jfields = "{\"Nombre\",\"TasaCambio\"}";
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X",
                TasaCambio = 3.4783M
            };

            // Ejecuta
            try
            {
                Assert.Throws<ApplicationException>(() => { moneda.ToJsonNoid(jfields); });
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }

        [Fact]
        public void TestToJsonNoid_ZBad03()
        {
            // Prepara (jfields no Id y algunos, y otros que no existen)
            string jfields = "[\"Nombre\",\"TasaCambio\",\"NoExiste\"]";
            Moneda moneda = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "X",
                TasaCambio = 3.4783M
            };

            // Ejecuta
            try
            {
                Assert.Throws<ArgumentException>(() => { moneda.ToJsonNoid(jfields); });
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }
    }
}
