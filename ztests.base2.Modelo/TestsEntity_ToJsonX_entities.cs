using System;
using Xunit;

using base2.Modelo;

namespace ztests.base2.Modelo
{
    public class TestsEntity_ToJsonX_entities
    {
        [Fact]
        public void TestToJsonX_entities_OK01()
        {
            // Prepara (entidades mismo tipo, jfields nulo)
            Moneda moneda1 = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "B",
                TasaCambio = 3.4783M
            };
            Moneda moneda2 = new Moneda
            {
                MonedaId = 2,
                Simbolo = "MD",
                Nombre = "Moneda Dos",
                Tipo = "X",
                TasaCambio = 2.3456M
            };
            Moneda moneda3 = new Moneda
            {
                MonedaId = 3,
                Simbolo = "MT",
                Nombre = "Moneda Tres",
                Tipo = "X",
                TasaCambio = 5.8321M
            };
            Entity[] monedas = { moneda1, moneda2, moneda3 };
            string jentitiesEsperadas = "[" + "{\"MonedaId\":1,\"Simbolo\":\"MUN\",\"Nombre\":\"Moneda Unica\"," +
                                                    "\"Tipo\":\"B\",\"TasaCambio\":3.4783}," +
                                                "{\"MonedaId\":2,\"Simbolo\":\"MD\",\"Nombre\":\"Moneda Dos\"," +
                                                    "\"Tipo\":\"X\",\"TasaCambio\":2.3456}," +
                                                "{\"MonedaId\":3,\"Simbolo\":\"MT\",\"Nombre\":\"Moneda Tres\"," +
                                                    "\"Tipo\":\"X\",\"TasaCambio\":5.8321}" +
                                        "]";

            // Ejecuta
            string jentities = Entity.ToJsonX(monedas);

            // Comprueba
            Assert.Equal(jentitiesEsperadas, jentities);
        }

        [Fact]
        public void TestToJsonX_entities_OK02()
        {
            // Prepara (entidades distintos tipos, jfields nulo)
            Moneda moneda1 = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "B",
                TasaCambio = 3.4783M
            };
            Cliente cliente = new Cliente
            {
                ClienteId = 100,
                Nombre = "Mario Bross",
                Telefono = "72007319",
                Saldo = 1200.34M
            };
            Moneda moneda3 = new Moneda
            {
                MonedaId = 3,
                Simbolo = "MT",
                Nombre = "Moneda Tres",
                Tipo = "X",
                TasaCambio = 5.8321M
            };
            Entity[] entities = { moneda1, cliente, moneda3 };
            string jentitiesEsperadas = "[" + "{\"MonedaId\":1,\"Simbolo\":\"MUN\",\"Nombre\":\"Moneda Unica\"," +
                                                    "\"Tipo\":\"B\",\"TasaCambio\":3.4783}," +
                                                "{\"ClienteId\":100,\"Nombre\":\"Mario Bross\"," +
                                                    "\"Telefono\":\"72007319\",\"Saldo\":1200.34}," +
                                                "{\"MonedaId\":3,\"Simbolo\":\"MT\",\"Nombre\":\"Moneda Tres\"," +
                                                    "\"Tipo\":\"X\",\"TasaCambio\":5.8321}" +
                                        "]";

            // Ejecuta
            string jentities = Entity.ToJsonX(entities);

            // Comprueba
            Assert.Equal(jentitiesEsperadas, jentities);
        }

        [Fact]
        public void TestToJsonX_entities_OK03()
        {
            // Prepara (array entidades vacío, jfields nulo)
            Entity[] entities = { };
            string jentitiesEsperadas = "[]";

            // Ejecuta
            string jentities = Entity.ToJsonX(entities);

            // Comprueba
            Assert.Equal(jentitiesEsperadas, jentities);
        }

        [Fact]
        public void TestToJsonX_entities_OK04()
        {
            // Prepara (entidades mismo tipo, jfields no nulo)
            Moneda moneda1 = new Moneda
            {
                MonedaId = 1,
                Simbolo = "MUN",
                Nombre = "Moneda Unica",
                Tipo = "B",
                TasaCambio = 3.4783M
            };
            Moneda moneda2 = new Moneda
            {
                MonedaId = 2,
                Simbolo = "MD",
                Nombre = "Moneda Dos",
                Tipo = "X",
                TasaCambio = 2.3456M
            };
            Moneda moneda3 = new Moneda
            {
                MonedaId = 3,
                Simbolo = "MT",
                Nombre = "Moneda Tres",
                Tipo = "X",
                TasaCambio = 5.8321M
            };
            Entity[] monedas = { moneda1, moneda2, moneda3 };
			string jfields = "[\"TasaCambio\",\"Simbolo\",\"Tipo\"]";
			string jentitiesEsperadas = "[" + "{\"MonedaId\":1,\"Nombre\":\"Moneda Unica\"}," +
				                                "{\"MonedaId\":2,\"Nombre\":\"Moneda Dos\"}," +
				                                "{\"MonedaId\":3,\"Nombre\":\"Moneda Tres\"}" +
                                        "]";

            // Ejecuta
            string jentities = Entity.ToJsonX(monedas, jfields);

            // Comprueba
            Assert.Equal(jentitiesEsperadas, jentities);
        }

        [Fact]
        public void TestToJsonX_entities_ZBad01()
        {
            // Prepara (array entidades nulo)
            Entity[] entities = null;

            // Ejecuta
            try
            {
                Assert.Throws<NullReferenceException>(() => Entity.ToJsonX(entities));
            }
            catch (Exception ex)
            {
                Assert.Equal("(No exception was thrown)", ex.Message);
            }
        }
    }
}
